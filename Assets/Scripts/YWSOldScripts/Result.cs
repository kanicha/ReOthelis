using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Result : MonoBehaviour
{
    [SerializeField] private Player1 p1;

    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

    private bool _repeatHit = false;

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
    }

    // Update is called once per frame
    void Update()
    {
        if (_repeatHit)
            return;
        else if (p1._ds4circle || Input.GetKeyDown(KeyCode.Space))
        {
            _repeatHit = true;
            Approval.Invoke();
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("GameEnd");
    }
}
