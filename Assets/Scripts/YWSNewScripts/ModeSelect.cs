using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private Player1 p1;

    int _selectCount = 0;
    private int _frameCount = 0;
    private int _moveSpeed = 10;
    private bool _repeatHit = false;
    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -171, 0);
        _selectCount = 0;
        
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
    }

    // Update is called once per frame
    void Update()
    {
        _frameCount++;
        _frameCount %= _moveSpeed;

        //�J�[�\����OFFLINE�ɍ��킹�Ă���A���I��{�^���������ꂽ�ꍇ�ɃV�[���J�ڂ�s��
        if (_repeatHit)
            return;
        else if (p1._ds4circle && _selectCount == 1 || Input.GetKeyDown(KeyCode.Space) && _selectCount == 1)
        {
            _repeatHit = true;
            Approval.Invoke();
        }


        if (_frameCount == 0)
        {
            //���L�[���͂ɍ��킹�ăJ�[�\������Ɉړ�������
            if (p1._vertical < 0 || Input.GetKeyDown(KeyCode.S))
            {
                if (_selectCount == 0)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196,-272, 0);
                    _selectCount++;
                }
                else if (_selectCount == 1)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -373, 0);
                    _selectCount++;
                }
                else if (_selectCount == 2)
                {
                    //��ԉ���ONLINE�ɍ��킹�Ă�ꍇ�͈�ԏ�ɖ߂�
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -171, 0);
                    _selectCount = 0;
                }
            }
            //��L�[���͂ɍ��킹�ăJ�[�\�����Ɉړ�������
            else if (p1._vertical > 0 || Input.GetKeyDown(KeyCode.W))
            {
                if (_selectCount == 0)
                {
                    //��ԏ��STORY�ɍ��킹�Ă�ꍇ�͈�ԉ��Ɉڂ�
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -373, 0);
                    _selectCount = 2;
                }
                else if (_selectCount == 1)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -171, 0);
                    _selectCount--;
                }
                else if (_selectCount == 2)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -272, 0);
                    _selectCount--;
                }
            }
        }
    }

    //���̃V�[���ɐi��
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}