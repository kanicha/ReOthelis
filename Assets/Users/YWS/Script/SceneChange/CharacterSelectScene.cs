using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScene : MonoBehaviour
{
    //上ボタンの取得
    [SerializeField] private Button _moveOn;
    public Button MoveOn
    {
        get { return _moveOn; }
    }

    //下ボタンの取得
    [SerializeField] private Button _goBack;
    public Button GoBack
    {
        get { return _goBack; }
    }

    private GameSceneManager _gameSceneManager;

    //次のシーンに進む
    public void MoveOnPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterEpisode");
    }

    //前のシーンに戻る
    public void GoBackPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Title");
    }

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        MoveOn.onClick.AddListener(() => MoveOnPush(_gameSceneManager));
        GoBack.onClick.AddListener(() => GoBackPush(_gameSceneManager));
    }
}
