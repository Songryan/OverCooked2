using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileSetrResolution : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    void Start()
    {
        AdjustCanvasScaler();
    }

    void AdjustCanvasScaler()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // 너비와 높이 균형 맞춤
        }
    }
}
