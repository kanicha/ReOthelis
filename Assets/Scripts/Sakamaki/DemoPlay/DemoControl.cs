using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DemoControl : Player1Base
{
    private GameSceneManager _gameSceneManager;
    private bool repeatHit = false;
    [SerializeField] private VideoPlayer _videoPlayer = null;
    
    private void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _videoPlayer.loopPointReached += LoopPointReached;
        _videoPlayer.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();
        
        if (repeatHit)
        {
            return;
        }

        // キーが押されたら推移
        if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space) && _gameSceneManager.IsChanged)
        {
            repeatHit = true;
            
            TitleSceneChange(_gameSceneManager);
        }
    }

    private void LoopPointReached(VideoPlayer vp)
    {
        repeatHit = true;
        
        TitleSceneChange(_gameSceneManager);
    }
    
    private void TitleSceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Title");
    }
}