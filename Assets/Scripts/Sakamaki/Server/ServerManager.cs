using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerManager : SingletonMonoBehaviour<ServerManager>
{
    private Thread _thread;
    
    // IPアドレス
    private string _ipAdress = "tlf93.synology.me";
    private int _portNumber = 3359;
    private TcpClient _tcpClient;
    private NetworkStream _streamKey;
    
    private string _message;

    // 自分のユーザーID
    public string _myId = "";
    
    // Start is called before the first frame update
    void Start()
    {
        InitServer();
        
        // 並行処理
        _thread = new Thread(ReceiveMessage);
        _thread.Start();
        
        RequestBase _requestBase = new RequestBase(RequestBase.PacketType.Matching);
        
        SendMessage(_requestBase);
    }

    /// <summary>
    /// サーバーの初期接続 初期化
    /// </summary>
    private void InitServer()
    {
        // インスタンス化
        _tcpClient = new TcpClient();
        // 初回接続(IPAdress, PortNumber)
        _tcpClient.Connect(_ipAdress, _portNumber);
        // NetWorkStreamを習得してくる
        _streamKey = _tcpClient.GetStream(); 
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
                    RequestBase jsonData = RequestBase.JsonToClass<RequestBase>(Encoding.UTF8.GetString(data));
                    Debug.Log(jsonData._packetType);
                }
            }
        }
        catch (SocketException e)
        {
            Debug.LogError("ServerError: " + e.ErrorCode);
        }
    }
}
