using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RequestBase
{
    // ユーザーid変数
    public string id = "";
    // 通信タイプ保存変数
    public string _packetType = "";
    
    // 通信タイプ
    public enum PacketType
    {
        Init,
        InitPiece,
        Matching,
        CharaConfirm,
        PieceMoved,
        StateChange,
        TurnChangeable,
        Send,
        Receive,
        End,
        Disconnect,
        OpponentDisconnect
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="packetType">通信のタイプ</param>
    public RequestBase(PacketType? packetType = null) // 型名の後ろに"?"をいれるとnull許容型になる (本来 null は代入できないができるようになる)
    {
        if (packetType != null)
            _packetType = packetType.ToString();
        
        Debug.Log(ServerManager.Instance._myId);
        id = ServerManager.Instance._myId;
    }

    /// <summary>
    /// 受け取ったRequestBase型のデータを文字列化にする関数
    /// </summary>
    /// <param name="requestBase">通信タイプ</param>
    /// <returns></returns>
    public static string ParseSendData(RequestBase requestBase)
    {
        return JsonUtility.ToJson(requestBase);
    }

    /// <summary>
    /// Jsonのデータを文字列型に変換する関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T JsonToClass<T>(string jsonData) where T : RequestBase // whereで RequestBase自身か、継承している物のみで指定する
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
