//using System.Threading;
//using Cysharp.Threading.Tasks;
//using UnityEngine.Events;

//public class MessageData
//{
//    private string _message;

//    private int _messagePos = 0;

//    private int _messageLength = 0;

//    private int _messageSpeed = 100;

//    private bool _isSkip = false;

//    private int _index = -1;

//    /// <summary>
//    /// メッセージの登録
//    /// </summary>
//    /// <param name="index">インデックス</param>
//    /// <param name="messge">登録するメッセージ</param>
//    /// <param name="messageSpeed">表示速度</param>
//    public MessageData(int index, string messge, int messageSpeed = 100)
//    {
//        this._index         = index;
//        this._message       = messge;
//        this._messageSpeed  = messageSpeed;
//        this._messagePos    = 0;
//        this._messageLength = messge.Length;
//        this._isSkip        = false;
//    }

//    /// <summary>
//    /// 文字のスキップ有効
//    /// </summary>
//    public void SetSkip()
//    {
//        _isSkip = true;
//    }

//    /// <summary>
//    /// メッセージのリクエスト
//    /// </summary>
//    /// <param name="messageCallback">取得するたびに呼び出されるコールバック関数</param>
//    public void GetDispMessage(UnityAction<string, bool> messageCallback, UnityAction<int> endCallback)
//    {
//        MessageLoopRead(messageCallback, endCallback, new CancellationToken()).Forget();
//    }

//    /// <summary>
//    /// メッセージ取得
//    /// </summary>
//    /// <param name="messagCallback">取得するたびに呼び出されるコールバック関数</param>
//    /// <param name="cancellationToken">キャンセル処理を呼び出す</param>
//    private async UniTask MessageLoopRead(UnityAction<string, bool> messagCallback, UnityAction<int> endCallback, CancellationToken cancellationToken)
//    {
//        if (null != messagCallback)
//        {
//            while (_messagePos < _messageLength)
//            {
//                _messagePos++;
//                if (_isSkip)
//                {
//                    _messagePos = _messageLength;
//                    messagCallback(_message, true);
//                }
//                else
//                    messagCallback(_message.Substring(0, _messagePos), _messagePos == _messageLength - 1);
//                await UniTask.Delay(_messageSpeed, false, PlayerLoopTiming.Update, cancellationToken);
//            }
//        }

//        if (null != endCallback)
//            endCallback(_index);
//    }
//}