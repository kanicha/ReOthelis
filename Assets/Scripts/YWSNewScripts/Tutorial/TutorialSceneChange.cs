using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneChange : Player1Base
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
        {
            return;
        }
        else if (TutorialDirector.Instance.tutorialPhase == TutorialDirector.TutorialPhase.End && (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space) || _DS4_cross_value || Input.GetKeyDown(KeyCode.Backspace)))
        {
            _repeatHit = true;
            SceneChange(_gameSceneManager);
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        SoundManager.Instance.StopSE();
        SoundManager.Instance.StopBGM();
        
        gameSceneManager.SceneNextCall("Title");
    }
}
