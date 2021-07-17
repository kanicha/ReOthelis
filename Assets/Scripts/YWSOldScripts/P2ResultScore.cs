using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ResultScore : MonoBehaviour
{
    //テキストに表示
    public Text scoreText = null;
    //スコアの初期値
    float score;

    void Start()
    {
        //ToStringでString型にしてテキストに表示
        scoreText.text = score.ToString();
        StartCoroutine(ScoreAnimation(30000, 0.5f));
    }

    // スコアをアニメーションさせる
    IEnumerator ScoreAnimation(float addScore, float time)
    {
        //前回のスコア
        float befor = score;
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
            scoreText.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        scoreText.text = after.ToString();
    }
}
