using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class TransformPacket 
{
    [Key(0)]
    public int type;

    [Key(1)]
    public float x;

    [Key(2)]
    public float y;
}
