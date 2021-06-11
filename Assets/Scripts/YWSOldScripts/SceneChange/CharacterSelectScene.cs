using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScene : MonoBehaviour
{
    //��{�^���̎擾
    [SerializeField] private Button _moveOn;
    public Button MoveOn
    {
        get { return _moveOn; }
    }

    //���{�^���̎擾
    [SerializeField] private Button _goBack;
    public Button GoBack
    {
        get { return _goBack; }
    }

    private GameSceneManager _gameSceneManager;

    //���̃V�[���ɐi��
    public void MoveOnPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterEpisode");
    }

    //�O�̃V�[���ɖ߂�
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
