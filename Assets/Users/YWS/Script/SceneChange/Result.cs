using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    //上ボタンの取得
    [SerializeField] private Button _retry;
    public Button Retry
    {
        get { return _retry; }
    }

    //下ボタンの取得
    [SerializeField] private Button _moveOn;
    public Button MoveOn
    {
        get { return _moveOn; }
    }

    private GameSceneManager _gameSceneManager;

    //次のシーンに進む
    public void RetryPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Game");
    }

    //前のシーンに戻る
    public void MoveOnPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterEpisode");
    }

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Retry.onClick.AddListener(() => RetryPush(_gameSceneManager));
        MoveOn.onClick.AddListener(() => MoveOnPush(_gameSceneManager));
    }
}
