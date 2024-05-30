using UnityEngine;

public class IngredientUI : MonoBehaviour
{
    public Transform target;
    [SerializeField] private new Vector3 pos = Vector3.zero;
    private void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + pos);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
