using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEpisode : MonoBehaviour
{
    //上ボタンの取得
    [SerializeField] private Button _startButton;
    public Button StartButton
    {
        get { return _startButton; }
    }

    //下ボタンの取得
    [SerializeField] private Button _endButton;
    public Button EndButton
    {
        get { return _endButton; }
    }

    private GameSceneManager _gameSceneManager;

    //次のシーンに進む
    public void StartGame(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Game");
    }

    //前のシーンに戻る
    public void EndGame(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("GameEnd");
    }

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        StartButton.onClick.AddListener(() => StartGame(_gameSceneManager));
        EndButton.onClick.AddListener(() => EndGame(_gameSceneManager));
    }
}
