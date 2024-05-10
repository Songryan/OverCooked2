using UnityEngine;

public class Ingredient : GameItem
{
    public enum IngredientType { Fish, Shrimp, Plate, Lettuce, Tomato, Cucumber, Chicken, Potato };
    public IngredientType type; // ��� ����: ä��, ��� ��
    
    public enum IngredientState { Raw, Cooking, Cooked }
    public IngredientState currentState = IngredientState.Raw;

    public override void Interact(Player player)
    {
        // �⺻ ��ȣ�ۿ�: ��Ḧ �ֿ�
        //Pickup(player);
    }

    // ���� ���� �޼ҵ�
    public void ChangeState(IngredientState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + currentState);
    }
}
