using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneChange : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameDirector _gameDirector;
    private bool _isGameEnd = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _gameDirector = FindObjectOfType<GameDirector>();
        _isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (_gameDirector.gameState == GameDirector.GameState.ended && _isGameEnd == false)
            {
                SceneChange(_gameSceneManager);
                _isGameEnd = true;
            }
            else if (_gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.G))
            {
                SkipGame(_gameSceneManager);
                _isGameEnd = true;
            }
        }
        catch (Exception e)
        {
            return;
        }
    }

    //次のシーンに進む
    private void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Result");
    }

    private void SkipGame(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterScenario_PB");
    }
}