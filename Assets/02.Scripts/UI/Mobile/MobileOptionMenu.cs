using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileOptionMenu : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite highlightSprite;
    public Sprite normalSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Application.platform != RuntimePlatform.Android)
            backgroundImage.sprite = highlightSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Application.platform != RuntimePlatform.Android)
            backgroundImage.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Application.platform != RuntimePlatform.Android)
            backgroundImage.sprite = normalSprite;
    }

    private void OnDisable()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            if (backgroundImage != null)
            {
                backgroundImage.sprite = normalSprite;
            }
        }
    }
}
