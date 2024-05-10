using UnityEngine;
using static CookingAppliance;

public class ETCTool : GameItem
{
    public enum ETCToolType { Sink, WasteBasket, Station, PlateTable };
    public ETCToolType toolType; // ���� ����: ��ũ��, ��������, ���������̼�, ���ý������̺� ��

    public enum ETCToolState { InUse, NotInUse }
    public ETCToolState currentState = ETCToolState.NotInUse;

    public override void Interact(Player player)
    {
        // �۾������ ��ȣ�ۿ� ���� ����
        Debug.Log("Player has interacted with a craft station.");
        // ���� ���, �������� �����ϰų� ����� Ȱ��ȭ�ϴ� ����
        //player.ActivateCraft(this.gameObject);
    }

    // ���� ���� �޼ҵ�
    public void ChangeState(ETCToolState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + currentState);
    }
}
