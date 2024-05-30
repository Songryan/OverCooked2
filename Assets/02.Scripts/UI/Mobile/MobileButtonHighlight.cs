using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backgroundImage;
    public Sprite highlightSprite;
    public Sprite normalSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 또는 터치가 버튼 위로 올라갔을 때 하이라이트 이미지를 설정합니다.
        backgroundImage.sprite = highlightSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 또는 터치가 버튼에서 나갔을 때 원래 이미지를 설정합니다.
        backgroundImage.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 후에도 normalSprite로 변경
        backgroundImage.sprite = normalSprite;
    }

    private void OnDisable()
    {
        // 오브젝트가 비활성화되기 전에 normalSprite로 변경
        if (backgroundImage != null)
        {
            backgroundImage.sprite = normalSprite;
        }
    }
}
