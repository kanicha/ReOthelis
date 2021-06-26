using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModeSelect : MonoBehaviour
{
    public RectTransform cursor;
    int selectCount = 0;
    private GameSceneManager _gameSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectCount == 0)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -101, 0);
                selectCount++;
            }
            else if (selectCount == 1)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -202, 0);
                selectCount++;
            }
            else if (selectCount == 2)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                selectCount = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectCount == 0)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -202, 0);
                selectCount = 2;
            }
            else if (selectCount == 1)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, 0, 0);
                selectCount--;
            }
            else if (selectCount == 2)
            {
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-210, -101, 0);
                selectCount--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && selectCount == 1)
        {
            SceneChange(_gameSceneManager);
        }
    }

    //éüÇÃÉVÅ[ÉìÇ…êiÇﬁ
    public void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}
