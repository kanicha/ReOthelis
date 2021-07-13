using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioScene : MonoBehaviour
{
    //ボタンの取得
    [SerializeField] private Button _button;
    public Button Button
    {
        get { return _button; }
    }

    private GameSceneManager _gameSceneManager;

    //次のシーンに進む
    public void ButtonPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Button.onClick.AddListener(() => ButtonPush(_gameSceneManager));
    }
}
