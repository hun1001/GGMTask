using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

public static class PacketManager
{
    public static byte[] Serialize<T>(T packet) where T : Packet
    {
        return MessagePackSerializer.Serialize(packet);
    }

    public static T Deserialize<T>(byte[] data) where T : Packet
    {
        return MessagePackSerializer.Deserialize<T>(data);
    }
}
