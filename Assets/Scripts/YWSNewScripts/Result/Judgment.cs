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
    //勝敗判定のテキストは2倍で出現し、0.8倍にまで縮小し、最後に1倍に戻る。
    //2倍の時のサイズ
    public int FirstSize = 300;
    //0.8倍の時のサイズ
    public int MiddleSize = 120;
    //1倍の時のサイズ
    public int FinalSize = 150;
    private bool IsDecreased = false;
    private bool IsIncreased = true;

    // Start is called before the first frame update
    void Start()
    {
        IsAppear = false;
        IsDecreased = false;
        IsIncreased = true;
        TextColor = 0;
        P1Judgment.color = new Color(0,0,0,0);
        P2Judgment.color = new Color(0,0,0,0);
        P1Judgment.fontSize = FirstSize;
        P2Judgment.fontSize = FirstSize;
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
        //テキストを透明から不透明に変更
        if (IsAppear == false && P1ResultScore.isScoreAppear == true && P2ResultScore.isScoreAppear == true)
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

        //テキストのサイズを変更
        if (IsDecreased == false && IsIncreased == true && P1ResultScore.isScoreAppear == true && P2ResultScore.isScoreAppear == true)
        {
            P1Judgment.fontSize = FirstSize;
            P2Judgment.fontSize = FirstSize;
            FirstSize -= 5;
            if (FirstSize <= MiddleSize)
            {
                FirstSize = MiddleSize;
                IsDecreased = true;
                IsIncreased = false;
            }
        }
        else if (IsDecreased == true && IsIncreased == false && P1ResultScore.isScoreAppear == true && P2ResultScore.isScoreAppear == true)
        {
            P1Judgment.fontSize = FirstSize;
            P2Judgment.fontSize = FirstSize;
            FirstSize += 5;
            if (FirstSize >= FinalSize)
            {
                FirstSize = FinalSize;
                IsDecreased = false;
                IsIncreased = false;
            }
        }
    }
}
