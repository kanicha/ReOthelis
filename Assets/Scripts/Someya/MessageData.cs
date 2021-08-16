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
//    /// ���b�Z�[�W�̓o�^
//    /// </summary>
//    /// <param name="index">�C���f�b�N�X</param>
//    /// <param name="messge">�o�^���郁�b�Z�[�W</param>
//    /// <param name="messageSpeed">�\�����x</param>
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
//    /// �����̃X�L�b�v�L��
//    /// </summary>
//    public void SetSkip()
//    {
//        _isSkip = true;
//    }

//    /// <summary>
//    /// ���b�Z�[�W�̃��N�G�X�g
//    /// </summary>
//    /// <param name="messageCallback">�擾���邽�тɌĂяo�����R�[���o�b�N�֐�</param>
//    public void GetDispMessage(UnityAction<string, bool> messageCallback, UnityAction<int> endCallback)
//    {
//        MessageLoopRead(messageCallback, endCallback, new CancellationToken()).Forget();
//    }

//    /// <summary>
//    /// ���b�Z�[�W�擾
//    /// </summary>
//    /// <param name="messagCallback">�擾���邽�тɌĂяo�����R�[���o�b�N�֐�</param>
//    /// <param name="cancellationToken">�L�����Z���������Ăяo��</param>
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