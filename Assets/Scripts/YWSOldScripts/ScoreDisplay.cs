using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text P1Score_Digit1;
    public Text P1Score_Digit2;
    public Text P1Score_Digit3;
    public Text P1Score_Digit4;
    public Text P1Score_Digit5;
    public Text P2Score_Digit1;
    public Text P2Score_Digit2;
    public Text P2Score_Digit3;
    public Text P2Score_Digit4;
    public Text P2Score_Digit5;

    private int P1Score = 29876;
    private int P2Score = 36975;
    private int RouletteDisplay;

    // Start is called before the first frame update
    void Start()
    {
        P1Score_Digit1.text = "0";
        P1Score_Digit2.text = "0";
        P1Score_Digit3.text = "0";
        P1Score_Digit4.text = "0";
        P1Score_Digit5.text = "0";
        P2Score_Digit1.text = "0";
        P2Score_Digit2.text = "0";
        P2Score_Digit3.text = "0";
        P2Score_Digit4.text = "0";
        P2Score_Digit5.text = "0";
        StartCoroutine(RouletteAnimation(P1Score,P2Score,0.5f));
    }

    IEnumerator RouletteAnimation(int P1Score, int P2Score, float time)
    {
        string P1FinalScore = P1Score.ToString();
        string P2FinalScore = P2Score.ToString();
        string[] P1ScoreStore = new string[5];
        string[] P2ScoreStore = new string[5];
        for (int i = 0; i < 5; i++)
        {
            P1ScoreStore[i] = P1FinalScore.Substring(i,1);
            //Debug.Log(P1ScoreStore[i]);
        }
        for (int i = 0; i < 5; i++)
        {
            P2ScoreStore[i] = P2FinalScore.Substring(i,1);
            //Debug.Log(P2ScoreStore[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            P1Score_Digit1.text = i.ToString();
            P1Score_Digit2.text = i.ToString();
            P1Score_Digit3.text = i.ToString();
            P1Score_Digit4.text = i.ToString();
            P1Score_Digit5.text = i.ToString();
            P2Score_Digit1.text = i.ToString();
            P2Score_Digit2.text = i.ToString();
            P2Score_Digit3.text = i.ToString();
            P2Score_Digit4.text = i.ToString();
            P2Score_Digit5.text = i.ToString();
        }
        yield return new WaitForSeconds(0.1f);
    }
}
