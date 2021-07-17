using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text P1Score;
    public Text P2Score;

    // Start is called before the first frame update
    void Start()
    {
        P1Score.text = "00000";
        P2Score.text = "00000";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
