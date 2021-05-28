using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    //��{�^���̎擾
    [SerializeField] private Button _retry;
    public Button Retry
    {
        get { return _retry; }
    }

    //���{�^���̎擾
    [SerializeField] private Button _moveOn;
    public Button MoveOn
    {
        get { return _moveOn; }
    }

    private GameSceneManager _gameSceneManager;

    //���̃V�[���ɐi��
    public void RetryPush(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("Game");
    }

    //�O�̃V�[���ɖ߂�
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
