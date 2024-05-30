using TMPro;
using UnityEngine;

public class MobileSpaceText : MonoBehaviour
{
    public TextMeshProUGUI spaceText;
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            spaceText.text = "터치해서 계속합니다";
        }
    }
}
