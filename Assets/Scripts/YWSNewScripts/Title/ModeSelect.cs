using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : Player1Base
{
    [SerializeField] private RectTransform cursor;
    [SerializeField, Header("デモプレイ画面に推移するまでの時間")] private float _demoPlayTime = 0.0f;
    [SerializeField] private Text[] modeText = new Text[4];
    
    public static int _selectCount = 0;
    private float _timeCount = 0.0f;
    private bool _repeatHit = false;
    private bool _isDemoChange = false;
    private GameSceneManager _gameSceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(1);

        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-220, -171, 0);
        _selectCount = 0;
        _isDemoChange = false;
        
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
            DemoPlayScemeChange(_gameSceneManager);
            // すでに推移したのでタイマーとフラグを初期化
            _timeCount = 0.0f;
            _isDemoChange = false;
        }
        
        if (_gameSceneManager.IsChanged && (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 0)
        {
            _timeCount = 0.0f;
            
            _repeatHit = true;
            SoundManager.Instance.PlaySE(9);
            CharacterSelectSceneChange(_gameSceneManager);
            
            _isDemoChange = true;
        }
        else if (_gameSceneManager.IsChanged && (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 1)
        {
            _repeatHit = true;
            SoundManager.Instance.PlaySE(9);
            CharacterSelectSceneChange(_gameSceneManager);
            
            _isDemoChange = true;
        }
        else if (_gameSceneManager.IsChanged && (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space)) && _selectCount == 2)
        {
            _repeatHit = true;
            SoundManager.Instance.PlaySE(9);
            SoundManager.Instance.StopBGM();

            OnlineLobbySceneChange(_gameSceneManager);
            
            _isDemoChange = true;
        }
        else if (_gameSceneManager.IsChanged &&
                 (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space) && _selectCount == 3))
        {
            _repeatHit = true;
            SoundManager.Instance.PlaySE(9);
            SoundManager.Instance.StopBGM();

            TutorialSceneChange(_gameSceneManager);

            _isDemoChange = true;
        }
        
        //下キーの入力に応じてカーソルを動かす
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0))
        {
            // タイムを初期化
            _timeCount = 0.0f;
            
            SoundManager.Instance.PlaySE(3);
            
            if (_selectCount == 0)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-220, -272, 0);
                _selectCount++;
            }
            else if (_selectCount == 1)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, -373, 0);
                _selectCount++;
            }
            else if (_selectCount == 2)
            {
                //カーソルが一番下にある場合で下キーが入力されたら、一番上に戻す
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-220, -171, 0);
                _selectCount++;
            }
            else if (_selectCount == 3)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 451, 0);
                _selectCount = 0;
            }
        }
        //上キーの入力に応じてカーソルを動かす
        else if ((_DS4_vertical_value > 0 && last_vertical_value == 0))
        {
            // タイムを初期化
            _timeCount = 0.0f;
            SoundManager.Instance.PlaySE(3);
            
            if (_selectCount == 0)
            {
                //カーソルが一番上にある場合で上キーが入力されたら、一番下のに戻す
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, -373, 0);
                _selectCount = 2;
            }
            else if (_selectCount == 1)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-220, -171, 0);
                _selectCount--;
            }
            else if (_selectCount == 2)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-220, -272, 0);
                _selectCount--;
            }
            else if (_selectCount == 3)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 451, 0);
                _selectCount--;
            }
        }
    }
    
    //キャラクター選択シーンへの遷移
    public void CharacterSelectSceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
    public void ScenarioSceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Scenario");
    }
    private void TutorialSceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("TutorialGame");
    }
    private void OnlineLobbySceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("OnlineLobby");
    }
    /// <summary>
    /// デモシーンに推移する関数
    /// </summary>
    /// <param name="gameSceneManager"></param>
    private void DemoPlayScemeChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("DemoPlay");
    }
}
