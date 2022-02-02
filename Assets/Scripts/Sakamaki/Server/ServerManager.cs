using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UniRx;
using UnityEngine;

public class ServerManager : SingletonMonoBehaviour<ServerManager>
{
    private Thread _thread;

    private Subject<object> _noticeData;

    // _onReceivedにgetを行う
    public IObservable<object> _onReceived => _noticeData;

    // 現在サーバーに繋がれているかどうか
    public static bool _isConnect { get; private set; }

    // IPアドレス
    private string _ipAdress = "tlf93.synology.me";
    private int _portNumber = 3359;
    private TcpClient _tcpClient;
    private NetworkStream _streamKey;
    
    private string _message;

    // 自分のユーザーID
    public string _myId = "";

    // プレイヤーの番号(自分が何Pなのか)
    public enum playerNumber
    {
        defaultPlayer,
        onePlayer,
        twoPlayer
    }
    public playerNumber myPlayerNumber { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        // サブジェクトをインスタンス化
        _noticeData = new Subject<object>();
        // 自分が1Pか2Pか割り当てる
        _onReceived.Subscribe(OnReceived).AddTo(this);
    }

    /// <summary>
    /// アプリ自身が終了されたときに行うイベント
    /// </summary>
    private void OnApplicationQuit()
    {
        // 切断
        Disconnect(true);
    }

    /// <summary>
    /// サーバーの初期接続 初期化
    /// </summary>
    public void InitServer()
    {
        // インスタンス化
        _tcpClient = new TcpClient();
        // 初回接続(IPAdress, PortNumber)
        _tcpClient.Connect(_ipAdress, _portNumber);
        // NetWorkStreamを習得してくる
        _streamKey = _tcpClient.GetStream();
        
        // 並行処理
        _thread = new Thread(ReceiveMessage);
        _thread.Start();
    }

    /// <summary>
    /// サーバーに文字列など送信する関数
    /// </summary>
    /// <param name="requestBase">送信するオブジェクト (RequestBase型))</param>
    public void SendMessage(RequestBase requestBase)
    {
        string sendMessage = RequestBase.ParseSendData(requestBase);

        // byte型変数を用意して、アスキーでエンコーディングを行う
        byte[] bytes = Encoding.ASCII.GetBytes(sendMessage);

        // データを送信する
        _streamKey.Write(bytes, 0, bytes.Length);

        Debug.Log(sendMessage);
    }

    /// <summary>
    /// サーバーからのデータ受け取り関数
    /// </summary>
    public void ReceiveMessage()
    {
        try
        {
            // DataAvailable, サーバーに読み取り可能なデータが有るかどうかを検知する(true || false で帰ってくる)
            // 読み取り可能なデータがあったらwhile
            while (true)
            {
                // byte型変数を用意 (TCPは8kbがデフォルトらしいので8kb)
                byte[] buffer = new byte[8192];
                // Readでbuffer分データを読み取りcountに代入
                int count = _streamKey.Read(buffer, 0, buffer.Length);

                // カウントが0より多かったら
                if (count > 0)
                {
                    byte[] data = new byte[count];

                    // Copy関数 (コピー元, コピー元の開始位置, コピー先, コピー先の開始位置, コピーする最大値)
                    // data変数にbufferのデータをcount分コピーしてくる
                    Array.Copy(buffer, 0, data, 0, count);

                    // 送られてきたデータをJson -> Class(文字列) に変換を行う
                    object jsonData = ParseRequest(Encoding.UTF8.GetString(data));
                    _noticeData.OnNext(jsonData);

                    Debug.Log(Encoding.UTF8.GetString(data));
                }
            }
        }
        catch (SocketException e)
        {
            Debug.LogError("ServerError: " + e.ErrorCode);
        }
    }

    /// <summary>
    /// 通信のタイプを文字列(string)からEnumにキャストを行う関数
    /// </summary>
    /// <returns></returns>
    public RequestBase.PacketType ParsePacketType(object packet)
    {
        RequestBase request = (RequestBase)packet;
        
        RequestBase.PacketType packetType =
            (RequestBase.PacketType)Enum.Parse(typeof(RequestBase.PacketType), request._packetType);

        return packetType;
    }

    /// <summary>
    /// 受信したらプレイヤーの番号とIDを割り当てる
    /// </summary>
    /// <param name="req">受信したデータ</param>
    private void OnReceived(object req)
    {
        RequestBase.PacketType packetType = ParsePacketType(req);

        switch (packetType)
        {
            case RequestBase.PacketType.Matching:
            {
                MatchingRequest matchingRequest = (MatchingRequest)req;

                switch (matchingRequest.isJoined)
                {
                    // 参加した時にIDと番号を割り当てる
                    case true:
                        myPlayerNumber = (playerNumber)matchingRequest.playerNumber;
                        _myId = matchingRequest.id;
                        break;
                }
            
                // 接続されたのでフラグを建てる
                _isConnect = true;
                break;
            }
            
            case RequestBase.PacketType.OpponentDisconnect:
                Debug.LogWarning("相手が切断されました");
                break;
        }
    }

    /// <summary>
    /// 動的にRequestをパケットタイプに応じた文字列に変換する
    /// </summary>
    /// <param name="jsonData">パケットデータ</param>
    /// <returns></returns>
    private object ParseRequest(string jsonData)
    {
        RequestBase requestBase = RequestBase.JsonToClass<RequestBase>(jsonData);
        RequestBase.PacketType packetType = ParsePacketType(requestBase);

        // パケットタイプの登録
        return packetType switch
        {
            RequestBase.PacketType.Matching => RequestBase.JsonToClass<MatchingRequest>(jsonData),
            RequestBase.PacketType.CharaConfirm => RequestBase.JsonToClass<CharaConfirmRequest>(jsonData),
            RequestBase.PacketType.OpponentDisconnect => RequestBase.JsonToClass<OpponentDisconnectRequest>(jsonData),
            RequestBase.PacketType.PieceMoved => RequestBase.JsonToClass<PieceMoveRequest>(jsonData),
            
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// 切断処理
    /// </summary>
    /// <param name="isNotifyOpponent">相手に通知するbool型変数</param>
    public void Disconnect(bool isNotifyOpponent)
    {
        DisconnectRequest disconnectRequest = new DisconnectRequest();
        disconnectRequest.isNotifyOpponent = isNotifyOpponent;
        
        // 送信
        SendMessage(disconnectRequest);
    }
}