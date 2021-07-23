using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : Player1Base
{
    [SerializeField] private RectTransform cursor;

    int _selectCount = 0;
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
        base.SaveKeyValue();
        base.KeyInput();

        //�J�[�\����OFFLINE�ɍ��킹�Ă���A���I��{�^���������ꂽ�ꍇ�ɃV�[���J�ڂ�s��
        if (_repeatHit)
            return;
        else if (_DS4_circle_value && _selectCount == 1 || Input.GetKeyDown(KeyCode.Space) && _selectCount == 1)
        {
            _repeatHit = true;
            Approval.Invoke();
        }

        //���L�[���͂ɍ��킹�ăJ�[�\������Ɉړ�������
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0))
        {
            if (_selectCount == 0)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -272, 0);
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
        else if ((_DS4_vertical_value > 0 && last_vertical_value == 0))
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

    //���̃V�[���ɐi��
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}
