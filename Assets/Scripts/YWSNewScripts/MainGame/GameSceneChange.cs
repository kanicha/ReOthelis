using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneChange : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private GameDirector _gameDirector;
    UnityEvent Approval = new UnityEvent();
    bool isGameEnd = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _gameDirector = FindObjectOfType<GameDirector>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameDirector.gameState == GameDirector.GameState.end && isGameEnd == false)
        {
            Approval.Invoke();
            isGameEnd = true;
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Result");
    }
}
