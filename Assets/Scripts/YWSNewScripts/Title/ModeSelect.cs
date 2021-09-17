using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : Player1Base
{
    [SerializeField] private RectTransform cursor;

    int _selectCount = 0;
    private bool _repeatHit = false;
    private GameSceneManager _gameSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(1);

        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -171, 0);
        _selectCount = 0;

        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        //カーソルがOFFLINEにある時に選択ボタンが押されたら遷移を行う
        if (_repeatHit)
            return;
        else if (_DS4_circle_value && _selectCount == 0 || Input.GetKeyDown(KeyCode.Space) && _selectCount == 0)
        {
            _repeatHit = true;
            GoToScenario(_gameSceneManager);
        }
        else if (_DS4_circle_value && _selectCount == 1 || Input.GetKeyDown(KeyCode.Space) && _selectCount == 1)
        {
            _repeatHit = true;
            GoToCharacterSelect(_gameSceneManager);
        }

        //下キーの入力に応じてカーソルを動かす
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
                //カーソルがONLINEにある場合で下キーが入力されたら、一番上のSTORYに戻す
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-196, -171, 0);
                _selectCount = 0;
            }
        }
        //上キーの入力に応じてカーソルを動かす
        else if ((_DS4_vertical_value > 0 && last_vertical_value == 0))
        {
            if (_selectCount == 0)
            {
                //カーソルがSTORYにある場合で上キーが入力されたら、一番下のONLINEに戻す
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

    //キャラクター選択シーンへの遷移
    public void GoToCharacterSelect(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }

    public void GoToScenario(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Scenario");
    }
}
