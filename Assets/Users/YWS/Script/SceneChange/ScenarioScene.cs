using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioScene : MonoBehaviour
{
    //�{�^���̎擾
    [SerializeField] private Button _button;
    public Button Button
    {
        get { return _button; }
    }

    private GameSceneManager _gameSceneManager;

    //���̃V�[���ɐi��
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
