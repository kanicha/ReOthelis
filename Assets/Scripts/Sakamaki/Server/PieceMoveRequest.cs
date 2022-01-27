using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoveRequest : RequestBase
{
    public PieceMoveRequest(Vector3 movedPos, Piece.PieceType pieceType, string pieceId) : base(RequestBase.PacketType
        .PieceMoved)
    {
        // 初期化
        piecePos = movedPos;
        pieceColor = (int) pieceType;
        this.pieceId = pieceId;
    }
    
    // コマの座標
    public Vector3 piecePos;
    // コマの色
    public int pieceColor;
    // コマのID
    public string pieceId = "";
}
