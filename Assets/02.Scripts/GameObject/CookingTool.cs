using UnityEngine;
using static CookingAppliance;

public class CookingTool : GameItem
{
    public enum ToolType { Pan, Pot, Frybasket, Plate, Extinguisher };
    public ToolType toolType; // ���� ����: ��������, ����, Ƣ���, ����, ��ȭ��? ��

    public enum CookingToolState { InUse, NotInUse }
    public CookingToolState currentState = CookingToolState.NotInUse;


    public override void Interact(Player player)
    {
        // �۾������ ��ȣ�ۿ� ���� ����
        Debug.Log("Player has interacted with a craft station.");
        // ���� ���, �������� �����ϰų� ����� Ȱ��ȭ�ϴ� ����
        //player.ActivateCraft(this.gameObject);
    }

    // ���� ���� �޼ҵ�
    public void ChangeState(CookingToolState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + currentState);
    }
}
