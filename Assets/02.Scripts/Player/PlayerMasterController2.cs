using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMasterController2 : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerList;

    [Header("Current Player")]
    public GameObject currentPlayer;

    [Header("Player Input")]
    public InputActionAsset inputActionAsset;

    [Header("particle")]
    public GameObject particle;

    [Header("Platform")]
    [SerializeField] private bool isMobile = false;

    [Header("Mobile Button")]
    public Button switchButton; // UI 버튼 참조

    private void Awake()
    {
        // Input System Asset을 공유하는 오브젝트가 있을 시 나중에 추가된 오브젝트가 입력을 받음
        // Player2가 Player1보다 나중에 추가되었기에 게임 시작 후 Player1이 움직이지 않는 버그가 존재하여 하단의 과정이 필요
        currentPlayer = playerList[0];
        currentPlayer.GetComponent<PlayerInput>().enabled = true;

        if (switchButton != null)
        {
            switchButton.onClick.AddListener(MobileSwitch); // 버튼 클릭 이벤트에 MobileSwitch 메서드 연결
        }
    }

    #region OnSwitch
    public void OnSwitch(InputValue inputValue)
    {
        SwitchPlayerComponent();
    }

    public void MobileSwitch()
    {
        SwitchPlayerComponent();
    }
    #endregion

    public void SwitchPlayerComponent()
    {
        // old player input 비활성화
        currentPlayer.GetComponent<PlayerInput>().enabled = false;
        currentPlayer.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        // 현재 플레이어의 인덱스를 가져온다.
        int currentIndex = playerList.IndexOf(currentPlayer);

        // 다음 플레이어의 인덱스를 계산한다.
        int nextIndex = (currentIndex + 1) % playerList.Count;

        if (particle != null)
        {
            GameObject particleInstance = Instantiate(particle, currentPlayer.transform.position, Quaternion.identity);
            particleInstance.transform.parent = gameObject.transform; // parentTransform은 넣고 싶은 부모 GameObject의 Transform입니다.
            SwitchParticle switchParticle = particleInstance.GetComponent<SwitchParticle>();
            switchParticle.SwitchPlayer(currentPlayer.transform, playerList[nextIndex].transform);
        }
        // 다음 플레이어를 currentPlayer로 설정한다.
        currentPlayer = playerList[nextIndex];

        currentPlayer.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        currentPlayer.GetComponent<PlayerInput>().enabled = true;   // new player input 활성화
    }
}
