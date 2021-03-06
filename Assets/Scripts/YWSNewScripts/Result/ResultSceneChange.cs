using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneChange : Player1Base
{
    private GameSceneManager _gameSceneManager;
    private bool _repeatHit = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        if (_repeatHit)
        {
            return;
        }
        else if (_gameSceneManager.IsChanged == true && _DS4_cross_value || _gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.Space))
        {
            _repeatHit = true;
            SoundManager.Instance.PlaySE(7);
            SceneChange(_gameSceneManager);
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        if (ModeSelect._selectCount == 0)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.StopSE();
            
            gameSceneManager.SceneNextCall("Scenario");
        }
        else
        {
            gameSceneManager.SceneNextCall("Title");
        }
    }
}
