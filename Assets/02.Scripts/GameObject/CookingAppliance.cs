using UnityEngine;

public class CookingAppliance : GameItem, ICookable
{
    public enum ApplianceType { ChoppingBoard, Stove, Oven, Fryer };
    public ApplianceType applianceType; // �ⱸ ����: ����, �����, ����, Ƣ��� ��

    public enum CookingApplianceState { InUse, NotInUse }
    public CookingApplianceState currentState = CookingApplianceState.NotInUse;

    public override void Interact(Player player)
    {
        //player.CookFood(this);
    }

    public void Cook(Player player)
    {
        throw new System.NotImplementedException();
    }

    // ���� ���� �޼ҵ�
    public void ChangeState(CookingApplianceState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + currentState);
    }
}
