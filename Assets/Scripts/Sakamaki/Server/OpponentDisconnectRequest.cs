using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentDisconnectRequest : RequestBase
{
    // コンストラクタ
    public OpponentDisconnectRequest(): base(PacketType.OpponentDisconnect) { }
    
}
