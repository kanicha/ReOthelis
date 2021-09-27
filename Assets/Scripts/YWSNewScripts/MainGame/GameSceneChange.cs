using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneChange : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameDirector _gameDirector;
    bool isGameEnd = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _gameDirector = FindObjectOfType<GameDirector>();
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameDirector.gameState == GameDirector.GameState.ended && isGameEnd == false)
        {
            SceneChange(_gameSceneManager);
            isGameEnd = true;
        }
        else if (_gameSceneManager.IsChanged == true && Input.GetKeyDown(KeyCode.G))
        {
            SkipGame(_gameSceneManager);
            isGameEnd = true;
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
