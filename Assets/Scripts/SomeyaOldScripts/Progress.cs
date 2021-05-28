using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行状態を管理するクラス
/// </summary>
public class Progress : MonoBehaviour
{
    public enum GameMode
    {
        Start,
        P1Select,
        P2Select,
        Turn,
        End,
        Interval,
    }
    static public Progress Instance;
    //現在のゲームモード
    public GameMode gameMode = GameMode.Start;

    //インターバル中にゲームモードを保管しておく場所
    GameMode nextGameMode = GameMode.Start;

    //ゲームモード変更からの経過時間
    [SerializeField]
    float time = 0;

    //インターバルの時間
    float intervalTime = 0;

    //入力外を持つゲームモードの終了フラグ
    public bool endGameMode = false;

    //反転するかどうかのフラグ
    public bool turnFlg = false;

    //反転終了後のターンプレイヤーの保存
    public int afterTurnPlayer = 0;

    // コントローラー制御
    // 後に変更
    //[SerializeField]
    //private GameObject Controller1P;
    //[SerializeField]
    //private GameObject Controller2P;

    private bool doOnce = true;
    // 後に画像を入れる
    //[SerializeField]
    //private GameObject InImage;
    //[SerializeField]
    //private GameObject turnImages;
    //[SerializeField]
    //private Sprite finishImage;
    //[SerializeField]
    //private Sprite senkouImage;
    public bool P1start = true;

    // Start is called before the first frame update
    void Start()
    {
        //InImage.SetActive(false);
        P1start = GameManager.order;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Instance.time += Time.deltaTime;

        switch (gameMode)
        {
            case GameMode.Start:
                Instance.StartUpdate();
                break;
            case GameMode.P1Select:
                Instance.P1SelectUpdate();
                break;
            case GameMode.P2Select:
                Instance.P2SelectUpdate();
                break;
            case GameMode.Turn:
                TurnUpdate();
                break;
            case GameMode.End:
                EndUpdate();
                break;
            case GameMode.Interval:
                Instance.IntervalUpdate();
                break;
        }
    }

    void TurnUpdate()
    {
        if(endGameMode)
        {
            ChagngeGameMode(afterTurnPlayer == 1 ? GameMode.P1Select : GameMode.P2Select, 1f);
        }
    }


    void StartUpdate()
    {
        if (time > 1)
        {
            if (P1start)
            {
                ChagngeGameMode(GameMode.P1Select, 1f);
            }
            else
            {
                ChagngeGameMode(GameMode.P2Select, 1f);
            }

        }
    }

    void P1SelectUpdate()
    {
        if(doOnce)
        {
            doOnce = false;
            if(GameManager.order)
            {
                //turnImages.transform.rotation = Quaternion.Euler(0, 0, 0);
                // ↓"senkouImage"を決まり次第変更
                //InImage.GetComponent<SpriteRenderer>().sprite = senkouImage;
            }
            if(!GameManager.order)
            {
                //turnImages.transform.rotation = Quaternion.Euler(0, 0, 0);
                // ↓"senkouImage"を決まり次第変更
                //InImage.GetComponent<SpriteRenderer>().sprite = senkouImage;
            }
        }
        if(endGameMode)
        {
            if (!turnFlg)
            {
                ChagngeGameMode(GameMode.P2Select, 1f);
            }
            else
            {
                afterTurnPlayer = 2;
                ChagngeGameMode(GameMode.Turn, 0.5f);
            }
        }
    }

    void P2SelectUpdate()
    {
        if (doOnce)
        {
            doOnce = false;
            if (GameManager.order)
            {
                //turnImages.transform.rotation = Quaternion.Euler(0, 0, 0);
                // ↓"senkouImage"を決まり次第変更
                //InImage.GetComponent<SpriteRenderer>().sprite = senkouImage;
            }
            if (!GameManager.order)
            {
                //turnImages.transform.rotation = Quaternion.Euler(0, 0, 0);
                // ↓"senkouImage"を決まり次第変更
                //InImage.GetComponent<SpriteRenderer>().sprite = senkouImage;
            }
        }
        if (endGameMode)
        {
            if (!turnFlg)
            {
                ChagngeGameMode(GameMode.P1Select, 1f);
            }
            else
            {
                afterTurnPlayer = 1;
                ChagngeGameMode(GameMode.Turn, 0.5f);
            }
        }
    }

    void EndUpdate()
    {
        if (doOnce)
        {
            doOnce = false;
            StartCoroutine(EndCor());
        }
    }

    private IEnumerator EndCor()
    {
        //turnImages.transform.rotation = Quaternion.Euler(0, 0, 0);
        //InImage.GetComponent<SpriteRenderer>().sprite = finishImage;
        yield break;
    }

    public void ChagngeGameMode(GameMode changeGameMode, float intervalSet = 0)
    {
        endGameMode = false;
        nextGameMode = changeGameMode;
        gameMode = GameMode.Interval;
        intervalTime = intervalSet;
        time = 0;
    }

    void P1SelectStart()
    {
        turnFlg = false;
        //↓サンプル
        //P1deckManager.Draw();
        //if (P1deckManager.OnHandLess())
        //{
        //    if (P2deckManager.OnHandLess())
        //    {
        //        ChagngeGameMode(GameMode.End, 0f);
        //    }
        //    else
        //    {
        //        ChagngeGameMode(GameMode.P2Select, 0f);
        //    }
        //}
    }

    void P2SelectStart()
    {
        turnFlg = false;
        //↓サンプル
        //P2deckManager.Draw();

        //if (P2deckManager.OnHandLess())
        //{
        //    if (P1deckManager.OnHandLess())
        //    {
        //        ChagngeGameMode(GameMode.End, 0f);
        //    }
        //    else
        //    {
        //        ChagngeGameMode(GameMode.P1Select, 0f);
        //    }
        //}
    }

    void IntervalUpdate()
    {
        if (time >= intervalTime)
        {
            gameMode = nextGameMode;
            time = 0;

            switch (gameMode)
            {
                case GameMode.P1Select:
                    // 後に変更
                    //Controller1P.SetActive(false);
                    //Controller2P.SetActive(true);
                    doOnce = true;
                    P1SelectStart();
                    break;
                case GameMode.P2Select:
                    //Controller1P.SetActive(true);
                    //Controller2P.SetActive(false);
                    doOnce = true;
                    P2SelectStart();
                    break;
                case GameMode.Turn:
                    break;
                case GameMode.End:
                    //Controller1P.SetActive(false);
                    //Controller2P.SetActive(false);
                    break;
                case GameMode.Interval:
                    break;
            }
        }
    }

    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield break;
    }
}
