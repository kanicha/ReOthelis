using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeRequest : RequestBase
{
    public StateChangeRequest(GameDirector.GameState gameState) : base(PacketType.StateChange)
    {
        // 初期化
        this.gameState = (int)gameState;
    }
    
    // State
    public int gameState;
}
