using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPieceRequest : RequestBase
{
    public InitPieceRequest(string[] initPieceIdArray) : base(RequestBase.PacketType
        .InitPiece)
    {
        // 初期化
        this._initPieceIdArray = initPieceIdArray;
    }

    // 初期コマのID用変数
    public string[] _initPieceIdArray;
}
