using UnityEngine;

public class Craft : GameItem
{
    public override void Interact(Player player)
    {
        // �۾������ ��ȣ�ۿ� ���� ����
        Debug.Log("Player has interacted with a craft station.");
        // ���� ���, �������� �����ϰų� ����� Ȱ��ȭ�ϴ� ����
        //player.ActivateCraft(this.gameObject);
    }
}
