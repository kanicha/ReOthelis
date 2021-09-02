using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgment : MonoBehaviour
{
    [SerializeField] private Image JudgeImage1P;
    [SerializeField] private Image JudgeImage2P;
    [SerializeField] private Sprite[] JudgeImageArray1P;
    [SerializeField] private Sprite[] JudgeImageArray2P;
    private float ImageColor = 0;
    private int Winner = 0;
    private float TextR = 1;
    private float TextG = 1;
    private float TextB = 1;
    private bool IsAppear = false;
    //private bool IsGotDark = false;
    //勝敗判定のテキストは2倍で出現し、0.8倍にまで縮小し、最後に1倍に戻る。
    private RectTransform P1Appear;
    private RectTransform P2Appear;
    //2倍の時のサイズ
    public int FirstSize = 300;
    /*public int FirstWidth_P1x = 800;
    public int FirstHeight_P1y = 500;
    public int FirstWidth_P2x = 800;
    public int FirstHeight_P2y = 400;*/
    //0.8倍の時のサイズ
    public int MiddleSize = 120;
    /*public int MiddleWidth_P1x = 320;
    public int MiddleHeight_P1y = 200;
    public int MiddleWidth_P2x = 320;
    public int MiddleHeight_P2y = 160;*/
    //1倍の時のサイズ
    public int FinalSize = 150;
    /*public int FinalWidth_P1x = 400;
    public int FinalHeight_P1y = 250;
    public int FinalWidth_P2x = 400;
    public int FinalHeight_p2y = 200;*/
    private bool IsDecreased = false;
    private bool IsIncreased = true;

    // Start is called before the first frame update
    void Start()
    {
        IsAppear = false;
        IsDecreased = false;
        IsIncreased = true;
        //IsGotDark = false;
        ImageColor = 0;
        JudgeImage1P.color = new Color(255,255,255,0);
        JudgeImage2P.color = new Color(255,255,255,0);
        /*P1Appear = GameObject.Find("JudgeImage1P").GetComponent<RectTransform>();
        P2Appear = GameObject.Find("JudgeImage2P").GetComponent<RectTransform>();
        P1Appear.sizeDelta = new Vector2(FirstWidth_P1x,FirstHeight_P1y);
        P2Appear.sizeDelta = new Vector2(FirstWidth_P2x,FirstHeight_P2y);*/
        
        if (Player_1.displayScore > Player_2.displayScore)
        {
            JudgeImage1P.sprite = JudgeImageArray1P[0];
            JudgeImage2P.sprite = JudgeImageArray2P[1];
            Winner = 1;
        }
        else if (Player_1.displayScore < Player_2.displayScore)
        {
            JudgeImage1P.sprite = JudgeImageArray1P[1];
            JudgeImage2P.sprite = JudgeImageArray2P[0];
            Winner = 2;
        }
        else
        {
            //P1Judgment.text = "DRAW";
            //P2Judgment.text = "DRAW";
            Winner = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //画像を透明から不透明に変更
        if (IsAppear == false && ScoreDisplay.IsScoreAppear == true)
        {
            JudgeImage1P.color = new Color(255,255,255,ImageColor);
            JudgeImage2P.color = new Color(255,255,255,ImageColor);
            ImageColor += Time.deltaTime;
            if (ImageColor >= 1)
            {
                ImageColor = 1;
                IsAppear = true;
            }
        }

        //画像のサイズを変更
        /*if (IsDecreased == false && IsIncreased == true && ScoreDisplay.IsScoreAppear == true)
        {
            //P1Judgment.fontSize = FirstSize;
            //P2Judgment.fontSize = FirstSize;
            P1Appear.sizeDelta = new Vector2(FirstWidth_P1x,FirstHeight_P1y);
            P2Appear.sizeDelta = new Vector2(FirstWidth_P2x,FirstHeight_P2y);
            FirstSize -= 5;
            if (FirstSize <= MiddleSize)
            {
                FirstSize = MiddleSize;
                IsDecreased = true;
                IsIncreased = false;
            }
        }
        else if (IsDecreased == true && IsIncreased == false && ScoreDisplay.IsScoreAppear == true)
        {
            //P1Judgment.fontSize = FirstSize;
            //P2Judgment.fontSize = FirstSize;
            FirstSize += 5;
            if (FirstSize >= FinalSize)
            {
                FirstSize = FinalSize;
                IsDecreased = false;
                IsIncreased = false;
            }
        }*/


        /*if (IsAppear == true && IsGotDark == false && Winner == 1)
        {
            P2Image.color = new Color(TextR,TextG,TextB);
            TextR -= Time.deltaTime;
            TextG -= Time.deltaTime;
            TextB -= Time.deltaTime;
            if (TextR <= 0.5f && TextG <= 0.5f && TextB <= 0.5f)
            {
                TextR = 0.5f;
                TextG = 0.5f;
                TextB = 0.5f;
                IsGotDark = true;
            } 
        }
        else if (IsAppear == true && IsGotDark == false && Winner == 2)
        {
            P1Image.color = new Color(TextR,TextG,TextB);
            TextR -= Time.deltaTime;
            TextG -= Time.deltaTime;
            TextB -= Time.deltaTime;
            if (TextR <= 0.5f && TextG <= 0.5f && TextB <= 0.5f)
            {
                TextR = 0.5f;
                TextG = 0.5f;
                TextB = 0.5f;
                IsGotDark = true;
            } 
        }*/
    }

}
