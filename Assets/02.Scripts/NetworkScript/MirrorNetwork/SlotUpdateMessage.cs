using Mirror;

public struct SlotUpdateMessage : NetworkMessage
{
    public int index;
    public bool isActive;
}