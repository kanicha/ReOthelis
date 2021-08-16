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
//    /// ���b�Z�[�W�̓o�^
//    /// </summary>
//    /// <param name="message">�\�����郁�b�Z�[�W</param>
//    /// <param name="speed">���x�ims�j</param>
//    /// <returns>�Ăяo�� ID</returns>
//    public int InitMessage(string message, int speed = 100)
//    {
//        var messid = _indexLists.FindIndex(x => false == x);
//        var messageData = new MessageData(messid, message);
//        _messageLists.Add(messid, messageData);
//        _indexLists[messid] = true;
//        return messid;
//    }

//    /// <summary>
//    /// �����X�L�b�v
//    /// </summary>
//    /// <param name="index">�Ăяo�� ID</param>
//    public void SkipMessage(int index)
//    {
//        _messageLists[index].SetSkip();
//    }

//    /// <summary>
//    /// ���b�Z�[�W�̃��N�G�X�g
//    /// </summary>
//    /// <param name="index">�Ăяo�� ID</param>
//    /// <param name="messageCallback">�擾���邽�тɌĂяo�����R�[���o�b�N�֐�</param>
//    public void StartDispMessage(int index, UnityAction<string, bool> messageCallback)
//    {
//        _messageLists[index].GetDispMessage(messageCallback, MessageEndCallback);
//    }

//    /// <summary>
//    /// �\�����I��������Ăяo�����
//    /// �Ώۂ�index��false�Adictionari���폜
//    /// </summary>
//    /// <param name="index">�폜�Ώۂ� ID</param>
//    private void MessageEndCallback(int index)
//    {
//        _messageLists.Remove(index);
//        _indexLists[index] = false;
//    }
//}
