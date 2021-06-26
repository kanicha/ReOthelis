using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : MonoBehaviour
{
    public RectTransform cursor;
    int selectCount = 0;
    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

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
        //���L�[���͂ɍ��킹�ăJ�[�\�������Ɉړ�������
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        //��L�[���͂ɍ��킹�ăJ�[�\������Ɉړ�������
        else if (Input.GetKeyDown(KeyCode.UpArrow))
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
        //�J�[�\����OFFLINE�ɍ��킹�Ă���A���I���{�^���������ꂽ�ꍇ�ɃV�[���J�ڂ��s��
        else if (Input.GetKeyDown(KeyCode.Space) && selectCount == 1)
        {
            Approval.Invoke();
        }
    }

    //���̃V�[���ɐi��
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}
