using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaConfirmRequest : RequestBase
{
    public CharaConfirmRequest() : base(PacketType.CharaConfirm) { }
    
    // プレイヤーが決定したかどうかの変数
    public bool isCharaConfirm = false;
    // 2人決定したかどうかの変数
    public bool isCompletedConfirm = false;
}
