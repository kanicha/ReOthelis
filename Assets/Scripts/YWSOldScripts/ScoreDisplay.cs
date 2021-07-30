using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    //プレイヤー１のスコアを表示するための一桁目から五桁目の変数
    public Text P1Score_Digit1;
    public Text P1Score_Digit2;
    public Text P1Score_Digit3;
    public Text P1Score_Digit4;
    public Text P1Score_Digit5;
    //プレイヤー２のスコアを表示するための一桁目から五桁目の変数
    public Text P2Score_Digit1;
    public Text P2Score_Digit2;
    public Text P2Score_Digit3;
    public Text P2Score_Digit4;
    public Text P2Score_Digit5;

    //プレイヤー１が獲得したスコア
    string P1FinalScore;
    //プレイヤー２が獲得したスコア
    string P2FinalScore;
    //プレイヤーの獲得スコアを5桁に分けるための配列
    string[] P1ScoreStore = new string[5];
    string[] P2ScoreStore = new string[5];
    //ルーレットを止めるためのフラグ
    bool IsRouletteEnd = false;
    //プレイヤーの獲得スコアの桁数を調べる変数
    int P1DigitNum = 0;
    int P2DigitNum = 0;
    //勝敗判定用のフラグ
    public static bool IsScoreAppear = false;

    // Start is called before the first frame update
    void Start()
    {
        Init(Player_1.displayScore,Player_2.displayScore);
        Invoke(nameof(ScoreRoulette), 1f);
    }

    void Init(int P1Score, int P2Score)
    {
        IsScoreAppear = false;
        //プレイヤーの獲得スコアを文字列に変換
        P1FinalScore = P1Score.ToString();
        P2FinalScore = P2Score.ToString();

        P1DigitNum = P1FinalScore.Length;
        P2DigitNum = P2FinalScore.Length;
        //桁数が４の場合、表示するスコアの前に０を一個加える
        if (P1DigitNum == 4)
        {
            P1FinalScore = "0" + P1Score.ToString();
        }
        if (P2DigitNum == 4)
        {
            P2FinalScore = "0" + P2Score.ToString();
        }

        IsRouletteEnd = false;
        //変換した文字列を配列に入れる
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
    }

    void Update()
    {
        //ルーレットを回す
        if (IsRouletteEnd == false)
        {
            P1Score_Digit1.text = Random.Range(0,9).ToString();
            P1Score_Digit2.text = Random.Range(0,9).ToString();
            P1Score_Digit3.text = Random.Range(0,9).ToString();
            P1Score_Digit4.text = Random.Range(0,9).ToString();
            P1Score_Digit5.text = Random.Range(0,9).ToString();
            P2Score_Digit1.text = Random.Range(0,9).ToString();
            P2Score_Digit2.text = Random.Range(0,9).ToString();
            P2Score_Digit3.text = Random.Range(0,9).ToString();
            P2Score_Digit4.text = Random.Range(0,9).ToString();
            P2Score_Digit5.text = Random.Range(0,9).ToString();
        }
    }

    //ルーレット終了時にスコアを表示する
    void ScoreRoulette()
    {
        P1Score_Digit1.text = P1ScoreStore[4].ToString();
        P1Score_Digit2.text = P1ScoreStore[3].ToString();
        P1Score_Digit3.text = P1ScoreStore[2].ToString();
        P1Score_Digit4.text = P1ScoreStore[1].ToString();
        P1Score_Digit5.text = P1ScoreStore[0].ToString();
        P2Score_Digit1.text = P2ScoreStore[4].ToString();
        P2Score_Digit2.text = P2ScoreStore[3].ToString();
        P2Score_Digit3.text = P2ScoreStore[2].ToString();
        P2Score_Digit4.text = P2ScoreStore[1].ToString();
        P2Score_Digit5.text = P2ScoreStore[0].ToString();
        IsRouletteEnd = true;
        IsScoreAppear = true;
    }
}
