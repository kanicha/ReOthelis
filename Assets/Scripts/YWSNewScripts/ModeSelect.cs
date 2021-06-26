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
        //下キー入力に合わせてカーソルを下に移動させる
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
                //一番下のONLINEに合わせてる場合は一番上に戻す
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                selectCount = 0;
            }
        }
        //上キー入力に合わせてカーソルを上に移動させる
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectCount == 0)
            {
                //一番上のSTORYに合わせてる場合は一番下に移す
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
        //カーソルがOFFLINEに合わせている、かつ選択ボタンが押された場合にシーン遷移を行う
        else if (Input.GetKeyDown(KeyCode.Space) && selectCount == 1)
        {
            Approval.Invoke();
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}
