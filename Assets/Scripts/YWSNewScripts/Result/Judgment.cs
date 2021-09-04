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
    private bool IsAppear = false;
    //勝敗判定のテキストは2倍で出現し、0.8倍にまで縮小し、最後に1倍に戻る。
    private RectTransform P1Appear;
    private RectTransform P2Appear;
    //2倍の時のサイズ
    public int FirstWidth_X = 800;
    public int FirstHeight_Y = 500;
    //0.8倍の時のサイズ
    public int MiddleWidth_X = 320;
    public int MiddleHeight_Y = 200;
    //1倍の時のサイズ
    public int FinalWidth_X = 400;
    public int FinalHeight_Y = 250;
    public int AppearSpeed = 5;
    private bool IsDecreased = false;
    private bool IsIncreased = true;

    // Start is called before the first frame update
    void Start()
    {
        IsAppear = false;
        IsDecreased = false;
        IsIncreased = true;
        ImageColor = 0;
        JudgeImage1P.color = new Color(255,255,255,0);
        JudgeImage2P.color = new Color(255,255,255,0);
        P1Appear = GameObject.Find("JudgeImage1P").GetComponent<RectTransform>();
        P2Appear = GameObject.Find("JudgeImage2P").GetComponent<RectTransform>();
        P1Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
        P2Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
        
        if (Player_1.displayScore > Player_2.displayScore)
        {
            JudgeImage1P.sprite = JudgeImageArray1P[0];
            JudgeImage2P.sprite = JudgeImageArray2P[1];
        }
        else if (Player_1.displayScore < Player_2.displayScore)
        {
            JudgeImage1P.sprite = JudgeImageArray1P[1];
            JudgeImage2P.sprite = JudgeImageArray2P[0];
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
        if (IsDecreased == false && IsIncreased == true && ScoreDisplay.IsScoreAppear == true)
        {
            P1Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
            P2Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
            FirstWidth_X -= AppearSpeed;
            FirstHeight_Y -= AppearSpeed;
            if (FirstWidth_X <= MiddleWidth_X && FirstHeight_Y <= MiddleHeight_Y)
            {
                FirstWidth_X = MiddleWidth_X;
                FirstHeight_Y = MiddleHeight_Y;
                IsDecreased = true;
                IsIncreased = false;
            }
        }
        else if (IsDecreased == true && IsIncreased == false && ScoreDisplay.IsScoreAppear == true)
        {
            P1Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
            P2Appear.sizeDelta = new Vector2(FirstWidth_X,FirstHeight_Y);
            FirstWidth_X += AppearSpeed;
            FirstHeight_Y += AppearSpeed;
            if (FirstWidth_X >= FinalWidth_X && FirstHeight_Y >= FinalHeight_Y)
            {
                FirstWidth_X = FinalWidth_X;
                FirstHeight_Y = FinalHeight_Y;
                IsDecreased = false;
                IsIncreased = false;
            }
        }
    }
}
