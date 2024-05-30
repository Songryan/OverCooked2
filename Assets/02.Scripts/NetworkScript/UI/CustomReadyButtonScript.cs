using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CustomReadyButtonScript : MonoBehaviour
{
    private Button readyButton;
    public Button player1Btn;
    public Button player2Btn;
    public Button player3Btn;
    public Button player4Btn;

    public ScrollSnapButton scrollSnapBtn1;
    public ScrollSnapButton scrollSnapBtn2;
    public ScrollSnapButton scrollSnapBtn3;
    public ScrollSnapButton scrollSnapBtn4;

    void Start()
    {
        readyButton = GetComponent<Button>();
        readyButton.onClick.AddListener(OnReadyButtonClicked);
    }

    void OnDestroy()
    {
        if (readyButton != null)
        {
            readyButton.onClick.RemoveListener(OnReadyButtonClicked);
        }
    }

    void OnReadyButtonClicked()
    {
        if (NetworkClient.active)
        {
            OverNetworkRoomPlayer[] players = FindObjectsOfType<OverNetworkRoomPlayer>();

            if (players == null)
                return;

            OverNetworkRoomPlayer localPlayer = null;

            foreach (var player in players)
            {
                if (player.isLocalPlayer)
                {
                    localPlayer = player;
                    break;
                }
            }

            if (localPlayer == null)
                return;

            if (localPlayer.readyToBegin)
            {
                // 레디 취소
                localPlayer.CmdChangeReadyState(false);
                transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("Client is not ready.");
                EnableButtons();
            }
            else
            {
                // 레디
                localPlayer.CmdChangeReadyState(true);
                transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Client is ready.");
                DisableButtons();
            }
        }
    }

    public void DisableButtons()
    {
        player1Btn.interactable = false;
        player2Btn.interactable = false;
        player3Btn.interactable = false;
        player4Btn.interactable = false;
    }

    public void EnableButtons()
    {
        player1Btn.interactable = true;
        player2Btn.interactable = true;
        player3Btn.interactable = true;
        player4Btn.interactable = true;
    }


    public void InitializingBattleRoom()
    {
        // 버튼 체크박스이미지 해제
        transform.GetChild(0).gameObject.SetActive(false);
        // 클릭 가능 하게
        EnableButtons();
        // 선택화면 오브젝트를 키고
        // 랜덤맵을 끄고
        // 버튼을 비활성화 시키고
        scrollSnapBtn1.SnapMapCancle();
        scrollSnapBtn2.SnapMapCancle();
        scrollSnapBtn3.SnapMapCancle();
        scrollSnapBtn4.SnapMapCancle();
    }
}
