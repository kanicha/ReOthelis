using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    //カウントアップが行われているかどうか
    private bool IsCounting = true;
    //カウントの初期値
    private float StartCount = 0.0f;
    //カウントの現在値
    private float NowCount = 0.0f;
    //カウントの最終値（プレイヤーの獲得スコア）
    private float Score = 0.0f;
    //必要時間
    private float RequiredTime = 0.5f;
    //現在経過時間
    private float CountedTime = 0.0f;

    private void Start()
    {
        IsCounting = true;
        StartCount = 0.0f;
        NowCount = 0.0f;
        Score = 0.0f;
        RequiredTime = 0.5f;
        CountedTime = 0.0f;
    }

    private void Update()
    {
        if (IsCounting == true)
        {
            if (NowCount > Score)
            {
                NowCount = Score;
                return;
            }
            else if (NowCount == Score)
            {
                IsCounting = false;
                return;
            }
            NowCount = StartCount + CountedTime / RequiredTime;
        }
        else
        {
            return;
        }
    }
}