using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingRequest : RequestBase
{
    // コンストラクタ
    public MatchingRequest() : base(PacketType.Matching) { }

    // プレイヤーが満員かどうか判別する変数
    public bool isFull = false;
    // 一人目のプレイヤーがはいってきたかどうか判別する変数
    public bool isJoined = false;
    // プレイヤーがマッチング完了したかどうか判別する変数
    public bool isCompleteMatching = false;
    // プレイヤーの番号変数
    public int playerNumber = 0;
}
