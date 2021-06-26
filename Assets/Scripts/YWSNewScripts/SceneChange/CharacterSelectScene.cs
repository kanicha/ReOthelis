using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelectScene : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    UnityEvent Approval = new UnityEvent();

    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        Approval.AddListener(() => SceneChange(_gameSceneManager));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Approval.Invoke();
        }
    }

    //éüÇÃÉVÅ[ÉìÇ…êiÇﬁ
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("GameSceme");
    }
}
