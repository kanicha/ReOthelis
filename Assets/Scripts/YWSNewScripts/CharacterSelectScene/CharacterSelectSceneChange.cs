using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelectSceneChange : Player1Base
{
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
        base.SaveKeyValue();
        base.KeyInput();

        if (_repeatHit)
            return;
        else if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space))
        {
            _repeatHit = true;
            Approval.Invoke();
        }
    }

    //次のシーンに進む
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("GameSceme");
    }
}
