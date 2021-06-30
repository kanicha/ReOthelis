using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : MonoBehaviour
{
    public RectTransform cursor;
    int selectCount = 0;
    private int _frameCount = 0;
    private int _moveSpeed = 10;
    private bool _repeatHit = false;
    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

    private float _vertical = 0.0f;
    private bool _ds4circle;

    // Start is called before the first frame update
    void Start()
    {
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
    }

    // Update is called once per frame
    void Update()
    {
        _frameCount++;
        _frameCount %= _moveSpeed;

        _vertical = Input.GetAxis("Vertical D-Pad");
        _ds4circle = Input.GetButtonDown("Fire_2");


        //�J�[�\����OFFLINE�ɍ��킹�Ă���A���I��{�^���������ꂽ�ꍇ�ɃV�[���J�ڂ�s��
        if (_repeatHit)
            return;
        else if (_ds4circle && selectCount == 1 || Input.GetKeyDown(KeyCode.Space))
        {
            _repeatHit = true;
            Approval.Invoke();
        }


        if (_frameCount == 0)
        {
            //���L�[���͂ɍ��킹�ăJ�[�\������Ɉړ�������
            if (_vertical < 0 || Input.GetKeyDown(KeyCode.S))
            {
                if (selectCount == 0)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -101, 0);
                    selectCount++;
                }
                else if (selectCount == 1)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -202, 0);
                    selectCount++;
                }
                else if (selectCount == 2)
                {
                    //��ԉ���ONLINE�ɍ��킹�Ă�ꍇ�͈�ԏ�ɖ߂�
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                    selectCount = 0;
                }
            }
            //��L�[���͂ɍ��킹�ăJ�[�\�����Ɉړ�������
            else if (_vertical > 0 || Input.GetKeyDown(KeyCode.W))
            {
                if (selectCount == 0)
                {
                    //��ԏ��STORY�ɍ��킹�Ă�ꍇ�͈�ԉ��Ɉڂ�
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -202, 0);
                    selectCount = 2;
                }
                else if (selectCount == 1)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                    selectCount--;
                }
                else if (selectCount == 2)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -101, 0);
                    selectCount--;
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