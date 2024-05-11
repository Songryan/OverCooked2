using UnityEngine;

// ���� �� �������� �⺻�� �����ϴ� �߻� Ŭ����
public abstract class GameItem : MonoBehaviour, IInteractable
{
    // ��� ���� �������� ������ �� �⺻ �Ӽ�
    public string itemName;
    //public Sprite icon;

    // IInteractable �������̽� ������ ������
    public abstract void Interact(Player player);
}