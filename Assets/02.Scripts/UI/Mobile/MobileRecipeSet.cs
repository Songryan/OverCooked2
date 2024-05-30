using TMPro;
using UnityEngine;

public class MobileRecipeSet : MonoBehaviour
{
    public TextMeshProUGUI recipeText;
    public GameObject recipeButtonUI;
    void Awake()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            recipeText.text = "터치해서 건너뛰기";
            recipeButtonUI.SetActive(false);
        }
    }
}
