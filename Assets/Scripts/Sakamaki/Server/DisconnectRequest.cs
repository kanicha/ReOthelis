using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectRequest : RequestBase
{
   // コンストラクタ
   public DisconnectRequest(): base(PacketType.Disconnect) { }

   // 相手が切断を通知する用のbool変数
   public bool isNotifyOpponent = false;
}
