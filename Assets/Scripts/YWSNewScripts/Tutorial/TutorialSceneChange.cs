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
        else if (/*TutorialDirector._isTutorialEnd == true &&*/ _gameSceneManager.IsChanged == true && _DS4_circle_value || 
                 /*TutorialDirector._isTutorialEnd == true &&*/ _gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.Space) ||
                 /*TutorialDirector._isTutorialEnd == true &&*/ _gameSceneManager.IsChanged == true && _DS4_cross_value || 
                 /*TutorialDirector._isTutorialEnd == true &&*/ _gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.X))
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
