using UnityEngine;

public class Ingredient : GameItem, IHandle
{
    public override void Interact(Player player)
    {
        // ������ ��ȣ�ۿ� ���� ����
        Debug.Log("Player has interacted with an ingredient.");
        // ���� ���, ��Ḧ �÷��̾��� �κ��丮�� �߰��ϴ� ���
        //player.PickUp(this.gameObject);
    }

    public void PickUp(Player player)
    {
        Debug.Log("Ingredient picked up by: " + player.name);
        // �÷��̾��� �κ��丮�� �տ� ������Ʈ�� �߰��ϴ� ����
        this.transform.parent = player.transform;  // ����: �÷��̾��� �ڽ����� ����
        this.gameObject.SetActive(false);  // �κ��丮�� �߰��Ǹ� ������ �ʰ� ó��
    }

    public void PutDown(Transform targetTransform)
    {
        Debug.Log("Ingredient put down");
        this.transform.parent = targetTransform;  // ��ǥ ��ġ�� ������Ʈ�� ��ġ
        this.gameObject.SetActive(true);  // �ٽ� ���̰� ����
    }
}
