using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ResultScore : MonoBehaviour
{
    //private Player_1 _player1 = null;
    public Text scoreText = null;
    //スコアの初期値
    float score;

    void Start()
    {
        //_player1 = FindObjectOfType<Player_1>();
        scoreText.text = score.ToString();
        StartCoroutine(ScoreAnimation(Player_1.displayScore, 0.5f));
    }

    // スコアをアニメーションさせる
    IEnumerator ScoreAnimation(float addScore, float time)
    {
        //前回のスコア
        float before = score;
        //今回のスコア
        float after = score + addScore;
        //得点加算
        score += addScore;
        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // テキストの更新
            scoreText.text = (before + (after - before) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        scoreText.text = after.ToString();
    }
}