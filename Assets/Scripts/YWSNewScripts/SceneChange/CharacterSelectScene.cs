using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelectScene : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

    private bool _repeatHit = false;
    private bool _ds4circle;
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
    }

    // Update is called once per frame
    void Update()
    {
        _ds4circle = Input.GetButtonDown("Fire_2");
        
        if (_repeatHit)
            return;
        else if (_ds4circle || Input.GetKeyDown(KeyCode.Space))
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
