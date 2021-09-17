//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class MessageManager : MonoBehaviour
//{
//    private Dictionary<int, MessageData> _messageLists = new Dictionary<int, MessageData>();
//    private List<bool> _indexLists = new List<bool>(new bool[100]);

//    private void Awake()
//    {
//        _indexLists.ForEach(x => x = false);
//    }

//    /// <summary>
//    /// メッセージの登録
//    /// </summary>
//    /// <param name="message">表示するメッセージ</param>
//    /// <param name="speed">速度（ms）</param>
//    /// <returns>呼び出し ID</returns>
//    public int InitMessage(string message, int speed = 100)
//    {
//        var messid = _indexLists.FindIndex(x => false == x);
//        var messageData = new MessageData(messid, message);
//        _messageLists.Add(messid, messageData);
//        _indexLists[messid] = true;
//        return messid;
//    }

//    /// <summary>
//    /// 文字スキップ
//    /// </summary>
//    /// <param name="index">呼び出し ID</param>
//    public void SkipMessage(int index)
//    {
//        _messageLists[index].SetSkip();
//    }

//    /// <summary>
//    /// メッセージのリクエスト
//    /// </summary>
//    /// <param name="index">呼び出し ID</param>
//    /// <param name="messageCallback">取得するたびに呼び出されるコールバック関数</param>
//    public void StartDispMessage(int index, UnityAction<string, bool> messageCallback)
//    {
//        _messageLists[index].GetDispMessage(messageCallback, MessageEndCallback);
//    }

//    /// <summary>
//    /// 表示が終了したら呼び出される
//    /// 対象のindexをfalse、dictionariを削除
//    /// </summary>
//    /// <param name="index">削除対象の ID</param>
//    private void MessageEndCallback(int index)
//    {
//        _messageLists.Remove(index);
//        _indexLists[index] = false;
//    }
//}
