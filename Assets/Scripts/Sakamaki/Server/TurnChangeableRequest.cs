using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnChangeableRequest : RequestBase
{
    public TurnChangeableRequest() : base(PacketType.TurnChangeable)
    {
    }
}
