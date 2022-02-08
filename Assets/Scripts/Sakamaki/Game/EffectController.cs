using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : SingletonMonoBehaviour<EffectController>
{
    [SerializeField, Header("どれぐらいの間隔で点滅するか")]
    private float _highLightTime = 0.0f;

    private float _time = 0.0f;
    
    // クイックローテートを使用したかどうかの変数
    public bool _isTurn = false;

    // スキルエフェクト関連変数
    
    
    /// <summary>
    /// 落下中のコマハイライト表示する関数
    /// </summary>
    /// <param name="active">true = on false = off</param>
    public void FallPieceHighLight(bool active, GameObject controlPiece)
    {
        // 軸のコマ情報の子についてあるハイライトするためのオブジェクトを参照
        GameObject controlPieceHighLightObj = controlPiece.transform.Find("highLightObj").gameObject;

        // エフェクトのオン
        if (active)
        {
            // コンポーネント習得
            Piece piece = controlPiece.GetComponent<Piece>();

            // 時間計測
            _time += Time.deltaTime;

            // 黒だった場合座標かクイックローテートを使った場合 ずらして表示をおこなう
            if (piece.pieceType == Piece.PieceType.black || _isTurn)
            {
                // 黒コマはエフェクトの座標が反転してしまうため y の値を増加させる
                Vector3 pos = controlPieceHighLightObj.transform.position;
                pos.y = -0.1f;
                controlPieceHighLightObj.transform.position = pos;

                if (_isTurn)
                    // フラグを初期化
                    _isTurn = false;
            }


            // 時間がハイライトの時間より大きくなったら
            if (_time > _highLightTime)
            {
                // falseだったらtimeを初期化した上でtrue
                if (controlPieceHighLightObj.activeSelf == false)
                {
                    controlPieceHighLightObj.SetActive(true);
                    _time = 0;
                }
                else
                {
                    controlPieceHighLightObj.SetActive(false);
                    _time = 0;
                }
            }
        }
        // エフェクトのオフ
        else
        {
            _time = 0;
            controlPieceHighLightObj.SetActive(false);
        }
    }
}