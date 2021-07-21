using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public RectTransform background;
    private int move = 1;
    private int distance = 0;

    void Update()
    {
        if (distance == 1900)
        {
            SceneManager.LoadScene("Result");
        }
        else
        {
            background.position += new Vector3(0, move, 0);
            distance = distance + move;
        }
    }
}