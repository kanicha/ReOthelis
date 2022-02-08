using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : Player1Base
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private int cursorPosition = -250;
    [SerializeField] private int tutorialCursorPosition = -300;
    
    [SerializeField, Header("デモプレイ画面に推移するまでの時間")]
    private float _demoPlayTime = 0.0f;

    [SerializeField] private Text[] modeText = new Text[4];

    private Vector2 textPos = Vector2.zero;
    public static int _selectCount = 0;
    private float _timeCount = 0.0f;
    private bool _repeatHit = false;
    private bool _isDemoChange = false;
    private GameSceneManager _gameSceneManager;

    enum ToSceneChange
    {
        CharacterSelect,
        TutorialGame,
        OnlineLobby,
        DemoPlay
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(1);

        _selectCount = 0;
        _isDemoChange = false;

        textPos = modeText[_selectCount].rectTransform.anchoredPosition;
        textPos.x = cursorPosition;

        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        //選択ボタンが押されたらカーソルが指しているモードに応じて遷移を行う
        if (_repeatHit)
            return;

        // 時間計測開始
        _timeCount += Time.deltaTime;

        // デモプレイの再生時間よりタイムのカウント(計測)がおおかったら推移
        if (_timeCount > _demoPlayTime && !_isDemoChange)
        {
            SoundManager.Instance.StopBGM();

            // デモプレイにシーンを推移
            SceneChange(ToSceneChange.DemoPlay, _gameSceneManager);
        }

        // 決定ボタン押された時の処理
        switch (_gameSceneManager.IsChanged)
        {
            case true when (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 0:
                SceneChange(ToSceneChange.CharacterSelect, _gameSceneManager);
                break;
            case true when (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 1:
                SceneChange(ToSceneChange.CharacterSelect, _gameSceneManager);
                break;
            case true when (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 2:
                SoundManager.Instance.StopBGM();

                SceneChange(ToSceneChange.OnlineLobby, _gameSceneManager);
                break;
            case true when (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space) && _selectCount == 3):
                SoundManager.Instance.StopBGM();

                SceneChange(ToSceneChange.TutorialGame, _gameSceneManager);
                break;
        }
        
        //入力に応じてカーソルを動かす
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0))
        {
            // タイムを初期化
            _timeCount = 0.0f;
            SoundManager.Instance.PlaySE(3);

            _selectCount++;
        }
        else if ((_DS4_vertical_value > 0 && last_vertical_value == 0))
        {
            // タイムを初期化
            _timeCount = 0.0f;
            SoundManager.Instance.PlaySE(3);

            _selectCount--;
        }
        
        switch (_selectCount)
        {
            case -1:
                _selectCount = 3;
                break;
            case 0:
                cursor.GetComponent<RectTransform>().anchoredPosition = textPos;
                break;
            case 1:
                cursor.GetComponent<RectTransform>().anchoredPosition = textPos;
                break;
            case 2:
                cursor.GetComponent<RectTransform>().anchoredPosition = textPos;
                break;
            case 3:
                //カーソルが一番下にある場合で下キーが入力されたら、一番上に戻す
                cursor.GetComponent<RectTransform>().anchoredPosition = textPos;
                break;
            case 4:
                _selectCount = 0;
                break;
        }

        // テキストのポジションをとってくる + 計算
        textPos = modeText[_selectCount].rectTransform.anchoredPosition;
        
        if (_selectCount == 3)
            textPos.x = tutorialCursorPosition;
        else
            textPos.x = cursorPosition;
    }

    /// <summary>
    /// シーンの遷移管理関数
    /// </summary>
    /// <param name="toSceneChange">どこの遷移先に行くか</param>
    /// <param name="gameSceneManager">マネージャー</param>
    private void SceneChange(ToSceneChange toSceneChange, GameSceneManager gameSceneManager)
    {
        // 代入先変数を用意
        string selectMode = "";
        
        // SEをならす
        SoundManager.Instance.PlaySE(9);
        
        // すでに推移したのでタイマーとフラグを初期化
        _timeCount = 0.0f;
        _repeatHit = true;

        switch (toSceneChange)
        {
            case ToSceneChange.CharacterSelect:
                selectMode = "CharacterSelect";
                break;
            case ToSceneChange.TutorialGame:
                selectMode = "TutorialGame";
                break;
            case ToSceneChange.OnlineLobby:
                selectMode = "OnlineLobby";
                break;
            case ToSceneChange.DemoPlay:
                selectMode = "DemoPlay";
                break;
            default:
                break;
        }

        // シーンの遷移
        gameSceneManager.SceneNextCall(selectMode);

        _isDemoChange = false;
    }
}