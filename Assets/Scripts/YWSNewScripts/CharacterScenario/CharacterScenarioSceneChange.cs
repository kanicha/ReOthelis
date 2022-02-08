using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScenarioSceneChange : Player1Base
{
    private GameSceneManager _gameSceneManager;
    private bool _repeatHit = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        if (_repeatHit)
            return;
        else if (CharacterScenario._isScenarioEnd == true && _gameSceneManager.IsChanged == true && _DS4_circle_value || 
                CharacterScenario._isScenarioEnd == true && _gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.Space) ||
                _gameSceneManager.IsChanged == true && _DS4_cross_value || _gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.X))
        {
            _repeatHit = true;
            SceneChange(_gameSceneManager);
        }
    }

    //次のシーンに進む
    private void SceneChange(GameSceneManager gameSceneManager)
    {
        SoundManager.Instance.StopSE();
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.StopStoryVoice();
        
        gameSceneManager.SceneNextCall("GameSceme");
    }
}
