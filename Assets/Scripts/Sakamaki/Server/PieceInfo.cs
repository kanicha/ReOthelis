using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceInfo
{
    public PieceInfo(MyVector3 movedPos, Piece.PieceType pieceType, string pieceId)
    {
        // 初期化
        piecePos = movedPos;
        pieceColor = (int)pieceType;
        this.pieceId = pieceId;
    }

    // コマの座標
    public MyVector3 piecePos;

    // コマの色
    public int pieceColor;

    // コマのID
    public string pieceId = "";
}