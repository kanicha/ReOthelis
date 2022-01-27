using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRequest : RequestBase
{
    public InitRequest() : base(PacketType.Init) { }
}
