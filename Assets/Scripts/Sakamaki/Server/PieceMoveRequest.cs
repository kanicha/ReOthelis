using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoveRequest : RequestBase
{
    public PieceMoveRequest(params PieceInfo[] pieceObjArray) : base(PacketType.PieceMoved)
    {
        // 初期化
        // 配列のコピー
        this.pieceObjArray = pieceObjArray;
    }

    public PieceMoveRequest(params GameObject[] pieceObjArray) : base(PacketType.PieceMoved)
    {
        // リストを宣言
        List<PieceInfo> pieceList = new List<PieceInfo>();

        foreach (GameObject gameObject in pieceObjArray)
        {
            // ピース型の変数を用意してgetComponentでインスタンス化
            Piece pieces = gameObject.GetComponent<Piece>();

            // リストに追加を行う
            pieceList.Add(new PieceInfo(pieces._myVector3, pieces.pieceType, pieces._pieceId));
        }

        this.pieceObjArray = pieceList.ToArray();
    }
    

    // コマの配列
    public PieceInfo[] pieceObjArray;
    // コマが生成されたかどうか変数
    public bool isCreated　= false;
}