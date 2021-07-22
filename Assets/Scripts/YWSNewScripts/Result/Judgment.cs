using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgment : MonoBehaviour
{
    public Text P1Judgment = null;
    public Text P2Judgment = null;
    private float TextColor = 0;
    private bool IsAppear = false;

    // Start is called before the first frame update
    void Start()
    {
        TextColor = 0;
        if (Player_1.displayScore > Player_2.displayScore)
        {
            P1Judgment.text = "WIN";
            P2Judgment.text = "LOSE";
        }
        else if (Player_1.displayScore < Player_2.displayScore)
        {
            P1Judgment.text = "LOSE";
            P2Judgment.text = "WIN";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAppear == false)
        {
            P1Judgment.color = new Color(0,0,0,TextColor);
            P2Judgment.color = new Color(0,0,0,TextColor);
            TextColor += Time.deltaTime;
            if (TextColor >= 1)
            {
                TextColor = 1;
                IsAppear = true;
            }
        }
    }
}
