using MessagePack;

[MessagePackObject]
public abstract class Packet
{
    [Key(0)]
    public abstract PacketType Type { get; }

}
