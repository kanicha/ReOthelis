using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : Player1Base
{
    private GameSceneManager _gameSceneManager;
    private bool repeatHit = false;

    private void Start()
    {
        // 初期化
        base.SaveKeyValue();
        base.KeyInput();

        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (repeatHit)
        {
            return;
        }

        // キーが押されたら推移
        if (_DS4_cross_value || Input.GetKeyDown(KeyCode.Space))
        {
            repeatHit = true;
            
            TitleSceneChange(_gameSceneManager);
        }
    }

    private void TitleSceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Title");
    }
}