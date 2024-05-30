using UnityEngine;

public class DestroyIngredient : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}