using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            return;
        else if (_DS4_triangle_value || Input.GetKeyDown(KeyCode.Space))
        {
            _repeatHit = true;
            SceneChange(_gameSceneManager);
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Title");
    }
}
