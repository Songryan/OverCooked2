using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;

/*
    문서: https://mirror-networking.gitbook.io/docs/components/network-room-manager
    API 참조: https://mirror-networking.com/docs/api/Mirror.NetworkRoomManager.html

    참조: NetworkManager
    문서: https://mirror-networking.gitbook.io/docs/components/network-manager
    API 참조: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

/// <summary>
/// 네트워크 룸을 포함한 특수한 NetworkManager입니다.
/// 룸에는 참가한 플레이어를 추적하는 슬롯과 최대 플레이어 수가 있습니다.
/// 룸 플레이어 객체에는 NetworkRoomPlayer 컴포넌트가 필요합니다.
/// NetworkRoomManager는 NetworkManager에서 파생되었으므로 NetworkManager 클래스에서 제공하는 많은 가상 함수를 구현합니다.
/// </summary>
public class OverNetworkRoomManager : NetworkRoomManager
{



    #region Game Object Spawn

    public GameObject GetCraftIngredient_RoomManager(Ingredient_Net.IngredientType it)
    {
        int index = 0;

        switch (it)
        {
            case Ingredient_Net.IngredientType.Cheese:
                index = 0; break;
            case Ingredient_Net.IngredientType.Chicken:
                index = 1; break;
            case Ingredient_Net.IngredientType.Cucumber:
                index = 2; break;
            case Ingredient_Net.IngredientType.Dough:
                index = 3; break;
            case Ingredient_Net.IngredientType.Fish:
                index = 4; break;
            case Ingredient_Net.IngredientType.Lettuce:
                index = 5; break;
            case Ingredient_Net.IngredientType.Meat:
                index = 6; break;
            case Ingredient_Net.IngredientType.Pepperoni:
                index = 7; break;
            case Ingredient_Net.IngredientType.PizzaTomato:
                index = 8; break;
            case Ingredient_Net.IngredientType.Potato:
                index = 9; break;
            case Ingredient_Net.IngredientType.Rice:
                index = 10; break;
            case Ingredient_Net.IngredientType.SeaWeed:
                index = 11; break;
            case Ingredient_Net.IngredientType.Shrimp:
                index = 12; break;
            case Ingredient_Net.IngredientType.SushiCucumber:
                index = 13; break;
            case Ingredient_Net.IngredientType.SushiFish:
                index = 14; break;
            case Ingredient_Net.IngredientType.SushiRice:
                index = 15; break;
            case Ingredient_Net.IngredientType.Tomato:
                index = 16; break;
            case Ingredient_Net.IngredientType.Tortilla:
                index = 17; break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(it), "Invalid ingredient type");
        }

        return Instantiate(spawnPrefabs[index]);
    }

    public GameObject GetUIObject_RoomManager(string UIObject)
    {
        int index = 0;

        switch (UIObject)
        {
            case "IngredientUI_Net":
                index = 18; break;
            case "Plate":
                index = 19; break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(UIObject), "Invalid ingredient type");
        }

        return spawnPrefabs[index];
    }

    #endregion

    #region Game Object Controller (처음알려준 이상한 방법)
    /*
    // 각 플레이어가 소유할 게임 오브젝트 배열
    public GameObject[] playerAObjects; // Player A가 소유할 게임 오브젝트들
    public GameObject[] playerBObjects; // Player B가 소유할 게임 오브젝트들

    // 클라이언트가 서버에 추가될 때 호출되는 메서드를 오버라이드합니다.
    public override void OnRoomServerAddPlayer(NetworkConnectionToClient conn)
    {
        // 기본 NetworkRoomManager의 OnRoomServerAddPlayer 메서드를 호출하여 기본 동작을 수행합니다.
        base.OnRoomServerAddPlayer(conn);

        // 클라이언트 연결 순서에 따라 오브젝트 소유권을 할당합니다.
        int playerIndex = conn.connectionId; // 연결된 클라이언트의 ID를 가져옵니다.

        // 첫 번째 플레이어에게 playerAObjects 소유권을 할당합니다.
        if (playerIndex == 0)
        {
            AssignOwnership(conn, playerAObjects);
        }
        // 두 번째 플레이어에게 playerBObjects 소유권을 할당합니다.
        else if (playerIndex == 1)
        {
            AssignOwnership(conn, playerBObjects);
        }
        // 예상치 못한 플레이어 인덱스일 경우 오류를 출력합니다.
        else
        {
            Debug.LogError("Unexpected player index");
        }
    }

    // 특정 클라이언트에게 오브젝트 소유권을 할당하는 메서드입니다.
    private void AssignOwnership(NetworkConnectionToClient conn, GameObject[] objects)
    {
        // 각 오브젝트에 대해 반복합니다.
        foreach (var obj in objects)
        {
            // 오브젝트에서 NetworkIdentity 컴포넌트를 가져옵니다.
            NetworkIdentity networkIdentity = obj.GetComponent<NetworkIdentity>();
            if (networkIdentity != null)
            {
                // NetworkIdentity가 있으면 해당 클라이언트에게 소유권을 할당합니다.
                networkIdentity.AssignClientAuthority(conn);
                Debug.Log("Assigned ownership of " + obj.name + " to client: " + conn.connectionId);
            }
            else
            {
                // NetworkIdentity가 없으면 오류를 출력합니다.
                Debug.LogError(obj.name + " does not have a NetworkIdentity component.");
            }
        }
    }
    */
    #endregion

    #region Update FixedUpdate
    /// <summary>
    /// 서버에서 룸 플레이어 객체를 생성하는 방법을 커스터마이징할 수 있습니다.
    /// <para>기본적으로 roomPlayerPrefab이 사용되지만, 이 함수를 통해 해당 동작을 커스터마이징할 수 있습니다.</para>
    /// </summary>
    /// <param name="conn">플레이어 객체의 연결.</param>
    /// <returns>새로운 룸 플레이어 객체.</returns>
    //private AsyncOperation operation;
    private float timer;
    private bool isLoading;
    public Image loadingBar;
    public GameObject LoadingKeyUI;
    public GameObject BattleUI;

    public GameObject[] canvasInPlayers; // Player positions in the canvas
    private OverNetworkRoomPlayer[] players; // Array to store the found players

    private void Update()
    {
        // Find all OverNetworkRoomPlayer objects in the scene
        players = FindObjectsOfType<OverNetworkRoomPlayer>();

        // Iterate through each player
        foreach (OverNetworkRoomPlayer player in players)
        {
            // Ensure the player's index is within the bounds of the canvasInPlayers array
            if (player.index >= 0 && player.index < canvasInPlayers.Length)
            {
                GameObject playerPosition = canvasInPlayers[player.index];

                // Check if the player position has exactly 2 child objects
                if (playerPosition.transform.childCount == 2)
                {
                    // Set the player's parent to the current player position
                    player.transform.SetParent(playerPosition.transform);
                    player.transform.localPosition = new Vector3(0f, 20f, 0f);

                    // Update the text to show the player's index + 1
                    playerPosition.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Player {player.index + 1}";
                }
            }
            else
            {
                Debug.LogWarning($"Player index {player.index} is out of bounds of the canvasInPlayers array.");
            }
        }
    }



    //public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    //{
    //    base.OnRoomServerConnect(conn);
    //    var player = Instantiate(spawnPrefabs[0]);
    //    NetworkServer.Spawn(player, conn);
    //    foreach (GameObject playerPosition in canvasInPlayers)
    //    {
    //        if (playerPosition.transform.childCount == 2)
    //            player.transform.SetParent(playerPosition.transform);
    //        player.transform.localPosition = new Vector3(0f, 20f, 0f);
    //    }
    //}

    /*
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // increment the index before adding the player, so first player starts at 1
        clientIndex++;

        if (Utils.IsSceneActive(RoomScene))
        {
            allPlayersReady = false;

            Debug.Log($"NetworkRoomManager.OnServerAddPlayer playerPrefab: {roomPlayerPrefab.name}");

            GameObject newRoomGameObject = OnRoomServerCreateRoomPlayer(conn);
            if (newRoomGameObject == null)
            {
                Debug.Log("Null 일때");
                newRoomGameObject = Instantiate(roomPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);
                foreach (GameObject playerPosition in canvasInPlayers)
                {
                    if (playerPosition.transform.childCount == 2)
                        newRoomGameObject.transform.SetParent(playerPosition.transform);
                }
            }
            else
            {
                Debug.Log("Null 아닐때");
                foreach (GameObject playerPosition in canvasInPlayers)
                {
                    if (playerPosition.transform.childCount == 2)
                    {
                        newRoomGameObject.transform.SetParent(playerPosition.transform);
                        break;
                    }
                }
            }

            NetworkServer.AddPlayerForConnection(conn, newRoomGameObject);
        }
        else
        {
            // Late joiners not supported...should've been kicked by OnServerDisconnect
            Debug.Log($"Not in Room scene...disconnecting {conn}");
            conn.Disconnect();
        }
    }
    */
    //void FixedUpdate()
    //{
    //    if (isLoading && loadingSceneAsync != null)
    //    {
    //        timer += Time.deltaTime;
    //
    //        if (loadingSceneAsync.progress < 0.9f)
    //        {
    //            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, loadingSceneAsync.progress, timer);
    //            if (loadingBar.fillAmount >= loadingSceneAsync.progress)
    //            {
    //                timer = 0f;
    //            }
    //        }
    //        else
    //        {
    //            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer);
    //            if (loadingBar.fillAmount == 1.0f)
    //            {
    //                loadingSceneAsync.allowSceneActivation = true;
    //                UIManager.Instance.LoadingFoodOff();
    //                isLoading = false;
    //            }
    //        }
    //    }
    //}


    public void MakeRoom()
    {
        StartHost();
    }

    public void JoinRoom()
    {
        StartClient();
    }
    void FixedUpdate()
    {
        if (isLoading && loadingSceneAsync != null)
        {
            timer += Time.deltaTime;

            // 로딩 속도를 증가시키기 위한 속도 배율
            float fakeLoadingSpeed = 2f; // 원하는 만큼 속도를 증가시킬 수 있습니다.

            if (loadingSceneAsync.progress < 0.9f)
            {
                // Lerp 속도 조정
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, loadingSceneAsync.progress, timer * fakeLoadingSpeed);
                if (loadingBar.fillAmount >= loadingSceneAsync.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                // 마지막 10%도 빠르게 채우기 위해 속도 조정
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timer * fakeLoadingSpeed);
                if (loadingBar.fillAmount == 1.0f)
                {
                    loadingSceneAsync.allowSceneActivation = true;
                    UIManager.Instance.LoadingFoodOff();
                    isLoading = false;
                }
            }
        }
    }
    #endregion

    #region Loding & Change Scene // 씬 변환때 적용될 Game Object Controller (GameScene에서만 적용)

    public void ReturnToRoomScene()
    {
        // RoomScene 이름을 설정하세요.
        string roomSceneName = GetRoomSceneName(); // RoomScene 이름을 실제 씬 이름으로 변경하세요.

        if (NetworkServer.active)
        {
            // 호스트일 경우
            Debug.Log("Host로 나가기");
            ServerChangeScene(roomSceneName);
        }
        else if (NetworkClient.isConnected)
        {
            // 클라이언트일 경우
            Debug.Log("Client로 나가기");
            ClientChangeScene(roomSceneName);
        }
        else
        {
            Debug.LogWarning("네트워크에 연결되어 있지 않습니다. RoomScene으로 돌아갈 수 없습니다.");
        }
    }

    // Player A가 소유할 게임 오브젝트들을 저장할 리스트
    private List<GameObject> playerAObjects = new List<GameObject>();

    // Player B가 소유할 게임 오브젝트들을 저장할 리스트
    private List<GameObject> playerBObjects = new List<GameObject>();

    protected override void ClientChangeScene(string newSceneName, SceneOperation sceneOperation = SceneOperation.Normal, bool customHandling = false)
    {
        // LodingBar Start
        Debug.Log("ClientChangeScene");
        LoadingKeyUI.SetActive(true);

        if (string.IsNullOrWhiteSpace(newSceneName))
        {
            Debug.LogError("ClientChangeScene empty scene name");
            return;
        }

        //Debug.Log($"ClientChangeScene newSceneName: {newSceneName} networkSceneName{networkSceneName}");

        // Let client prepare for scene change
        OnClientChangeScene(newSceneName, sceneOperation, customHandling);

        // After calling OnClientChangeScene, exit if server since server is already doing
        // the actual scene change, and we don't need to do it for the host client
        if (NetworkServer.active)
            return;

        // set client flag to stop processing messages while loading scenes.
        // otherwise we would process messages and then lose all the state
        // as soon as the load is finishing, causing all kinds of bugs
        // because of missing state.
        // (client may be null after StopClient etc.)
        // Debug.Log("ClientChangeScene: pausing handlers while scene is loading to avoid data loss after scene was loaded.");
        NetworkClient.isLoadingScene = true;

        // Cache sceneOperation so we know what was requested by the
        // Scene message in OnClientChangeScene and OnClientSceneChanged
        clientSceneOperation = sceneOperation;

        // scene handling will happen in overrides of OnClientChangeScene and/or OnClientSceneChanged
        // Do not call FinishLoadScene here. Custom handler will assign loadingSceneAsync and we need
        // to wait for that to finish. UpdateScene already checks for that to be not null and isDone.
        if (customHandling)
            return;

        // LodingBar Setting
        isLoading = true;
        timer = 0f;
        UIManager.Instance.LoadingFood();

        switch (sceneOperation)
        {
            case SceneOperation.Normal:
                loadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);
                break;
            case SceneOperation.LoadAdditive:
                // Ensure additive scene is not already loaded on client by name or path
                // since we don't know which was passed in the Scene message
                if (!SceneManager.GetSceneByName(newSceneName).IsValid() && !SceneManager.GetSceneByPath(newSceneName).IsValid())
                    loadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
                else
                {
                    Debug.LogWarning($"Scene {newSceneName} is already loaded");

                    // Reset the flag that we disabled before entering this switch
                    NetworkClient.isLoadingScene = false;
                }
                break;
            case SceneOperation.UnloadAdditive:
                // Ensure additive scene is actually loaded on client by name or path
                // since we don't know which was passed in the Scene message
                if (SceneManager.GetSceneByName(newSceneName).IsValid() || SceneManager.GetSceneByPath(newSceneName).IsValid())
                    loadingSceneAsync = SceneManager.UnloadSceneAsync(newSceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                else
                {
                    Debug.LogWarning($"Cannot unload {newSceneName} with UnloadAdditive operation");

                    // Reset the flag that we disabled before entering this switch
                    NetworkClient.isLoadingScene = false;
                }
                break;
        }

        // don't change the client's current networkSceneName when loading additive scene content
        if (sceneOperation == SceneOperation.Normal)
            networkSceneName = newSceneName;

        // LodingBar Start
        loadingBar.fillAmount = 0f;
        //switch (newSceneName)
        //{
        //    case "Assets/01.Scenes/Inseok/NetworkScenes/BattleLobby_Net.unity":
        //        break;
        //    default:
        //        Debug.Log("ClientChangeScene LoadingKeyUI / BattleUI Off");
        //        LoadingKeyUI.SetActive(false);
        //        BattleUI.SetActive(false);
        //        break;
        //}

        // GameScene으로 전환될 때 소유권 할당
        if (newSceneName == GameplayScene)
        {
            //AssignOwnershipToPlayers();
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        Debug.Log("ServerChangeScene");
        LoadingKeyUI.SetActive(true);

        base.ServerChangeScene(newSceneName);

        if (string.IsNullOrWhiteSpace(newSceneName))
        {
            Debug.LogError("ServerChangeScene 빈 장면 이름");
            return;
        }

        if (NetworkServer.isLoadingScene && newSceneName == networkSceneName)
        {
            Debug.LogError($"장면 변경이 이미 진행 중입니다. {newSceneName}");
            return;
        }

        if (!NetworkServer.active && newSceneName != offlineScene)
        {
            Debug.LogError("ServerChangeScene은 활성 서버에서만 호출할 수 있습니다.");
            return;
        }

        NetworkServer.SetAllClientsNotReady();
        networkSceneName = newSceneName;

        // 서버가 장면 변경을 준비하도록 합니다.
        OnServerChangeScene(newSceneName);

        // 장면을 변경하는 동안 메시지 처리를 중지하도록 서버 플래그를 설정합니다.
        // FinishLoadScene에서 다시 활성화됩니다.
        NetworkServer.isLoadingScene = true;

        //Debug.Log($"newSceneName : {newSceneName}");
        //
        //// 커스텀 씬 전환 로직
        //switch (newSceneName)
        //{
        //    case "Assets/01.Scenes/Inseok/NetworkScenes/BattleLobby_Net.unity":
        //        UIManager.Instance.EnterLoadingKeyUIBattle();
        //        break;
        //    // 필요한 다른 씬에 대한 케이스 추가
        //    default:
        //        Debug.LogWarning($"No custom transition logic defined for scene {newSceneName}");
        //        break;
        //}

        if (isLoading) return; // 이미 로딩 중이면 중복 호출 방지

        isLoading = true;
        timer = 0f;
        // 여기
        //UIManager.Instance.EnterLoadingKeyUI();
        UIManager.Instance.LoadingFood();
        loadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);
        loadingSceneAsync.allowSceneActivation = false;

        // 서버를 중지할 때 ServerChangeScene을 호출할 수 있습니다.
        // 이런 일이 발생하면 서버는 활성화되지 않으므로 클라이언트에게 변경 사항을 알릴 필요가 없습니다.
        if (NetworkServer.active)
        {
            // notify all clients about the new scene
            NetworkServer.SendToAll(new SceneMessage
            {
                sceneName = newSceneName
            });
        }

        startPositionIndex = 0;
        startPositions.Clear();

        //switch (newSceneName)
        //{
        //    case "Assets/01.Scenes/Inseok/NetworkScenes/BattleLobby_Net.unity":
        //        break;
        //    default:
        //        Debug.Log("ServerChangeScene LoadingKeyUI / BattleUI Off");
        //        LoadingKeyUI.SetActive(false);
        //        //BattleUI.SetActive(false);
        //        break;
        //}
        // GameScene으로 전환될 때 소유권 할당
        if (newSceneName == GameplayScene)
        {
            //AssignOwnershipToPlayers();
        }
    }

    // 현재 씬이 게임 씬인지 확인하는 메소드
    public bool IsCurrentSceneGameScene()
    {
        return SceneManager.GetActiveScene().name == GameplayScene;
    }

    // RoomScene 이름을 반환하는 메소드
    private string GetRoomSceneName()
    {
        return RoomScene;
    }
    #endregion

    /*
    #region 서버 콜백

    /// <summary>
    /// 서버가 시작될 때 호출됩니다 - 호스트가 시작될 때도 포함됩니다.
    /// </summary>
    public override void OnRoomStartServer() { }

    /// <summary>
    /// 서버가 중지될 때 호출됩니다 - 호스트가 중지될 때도 포함됩니다.
    /// </summary>
    public override void OnRoomStopServer() { }

    /// <summary>
    /// 호스트가 시작될 때 호출됩니다.
    /// </summary>
    public override void OnRoomStartHost() { }

    /// <summary>
    /// 호스트가 중지될 때 호출됩니다.
    /// </summary>
    public override void OnRoomStopHost() { }

    /// <summary>
    /// 새로운 클라이언트가 서버에 연결될 때 호출됩니다.
    /// </summary>
    /// <param name="conn">새로운 연결.</param>
    public override void OnRoomServerConnect(NetworkConnectionToClient conn) { }

    /// <summary>
    /// 클라이언트가 연결을 끊을 때 호출됩니다.
    /// </summary>
    /// <param name="conn">연결이 끊어진 클라이언트.</param>
    public override void OnRoomServerDisconnect(NetworkConnectionToClient conn) { }

    /// <summary>
    /// 네트워크 씬 로딩이 완료되었을 때 서버에서 호출됩니다.
    /// </summary>
    /// <param name="sceneName">새로운 씬의 이름.</param>
    public override void OnRoomServerSceneChanged(string sceneName) { }

    /// <summary>
    /// 서버에서 룸 플레이어 객체를 생성하는 방법을 커스터마이징할 수 있습니다.
    /// <para>기본적으로 roomPlayerPrefab이 사용되지만, 이 함수를 통해 해당 동작을 커스터마이징할 수 있습니다.</para>
    /// </summary>
    /// <param name="conn">플레이어 객체의 연결.</param>
    /// <returns>새로운 룸 플레이어 객체.</returns>
    public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
    {
        return base.OnRoomServerCreateRoomPlayer(conn);
    }

    /// <summary>
    /// 서버에서 게임 플레이어 객체를 생성하는 방법을 커스터마이징할 수 있습니다.
    /// <para>기본적으로 gamePlayerPrefab이 사용되지만, 이 함수를 통해 해당 동작을 커스터마이징할 수 있습니다. 함수에서 반환된 객체는 연결된 룸 플레이어를 대체하는 데 사용됩니다.</para>
    /// </summary>
    /// <param name="conn">플레이어 객체의 연결.</param>
    /// <param name="roomPlayer">이 연결의 룸 플레이어 객체.</param>
    /// <returns>새로운 게임 플레이어 객체.</returns>
    public override GameObject OnRoomServerCreateGamePlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
    {
        return base.OnRoomServerCreateGamePlayer(conn, roomPlayer);
    }

    /// <summary>
    /// 서버에서 게임 플레이어 객체를 생성하는 방법을 커스터마이징할 수 있습니다.
    /// <para>이 함수는 첫 번째 게임 플레이 씬 이후의 추가적인 씬에서만 호출됩니다.</para>
    /// <para>초기 게임 플레이 씬의 플레이어 객체를 커스터마이징하려면 OnRoomServerCreateGamePlayer를 참조하세요.</para>
    /// </summary>
    /// <param name="conn">플레이어 객체의 연결.</param>
    public override void OnRoomServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnRoomServerAddPlayer(conn);
    }

    /// <summary>
    /// 클라이언트가 룸 씬에서 게임 플레이 씬으로 전환을 완료했을 때 서버에서 호출됩니다.
    /// <para>룸에서 전환할 때, 룸 플레이어는 게임 플레이어 객체로 교체됩니다. 이 콜백 함수는 룸 플레이어의 상태를 게임 플레이어 객체에 적용할 기회를 제공합니다.</para>
    /// </summary>
    /// <param name="conn">플레이어의 연결</param>
    /// <param name="roomPlayer">룸 플레이어 객체.</param>
    /// <param name="gamePlayer">게임 플레이어 객체.</param>
    /// <returns>룸 플레이어를 교체하는 것을 허용하지 않으려면 false를 반환.</returns>
    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        return base.OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer);
    }

    /// <summary>
    /// 클라이언트가 준비 상태를 변경할 때 NetworkRoomPlayer.CmdChangeReadyState에서 서버로부터 호출됩니다.
    /// </summary>
    public override void ReadyStatusChanged()
    {
        base.ReadyStatusChanged();
    }

    /// <summary>
    /// 룸에 있는 모든 플레이어가 준비되었을 때 서버에서 호출됩니다.
    /// <para>기본 구현은 ServerChangeScene()을 사용하여 게임 플레이어 씬으로 전환합니다. 이 콜백을 구현하면 모든 플레이어가 준비되었을 때 카운트다운이나 그룹 리더의 확인 등 맞춤형 동작을 할 수 있습니다.</para>
    /// </summary>
    public override void OnRoomServerPlayersReady()
    {
        base.OnRoomServerPlayersReady();
    }

    /// <summary>
    /// CheckReadyToBegin에서 플레이어가 준비되지 않았을 때 서버에서 호출됩니다.
    /// <para>준비되지 않은 플레이어가 계속 참여할 때 여러 번 호출될 수 있습니다.</para>
    /// </summary>
    public override void OnRoomServerPlayersNotReady() { }

    #endregion

    #region 클라이언트 콜백

    /// <summary>
    /// 게임 클라이언트가 룸에 들어갈 때 사용자 정의 동작을 허용하는 훅입니다.
    /// </summary>
    public override void OnRoomClientEnter() { }

    /// <summary>
    /// 게임 클라이언트가 룸을 나갈 때 사용자 정의 동작을 허용하는 훅입니다.
    /// </summary>
    public override void OnRoomClientExit() { }

    /// <summary>
    /// 클라이언트가 서버에 연결될 때 호출됩니다.
    /// </summary>
    public override void OnRoomClientConnect() { }

    /// <summary>
    /// 클라이언트가 서버와의 연결이 끊어졌을 때 호출됩니다.
    /// </summary>
    public override void OnRoomClientDisconnect() { }

    /// <summary>
    /// 클라이언트가 시작될 때 호출됩니다.
    /// </summary>
    public override void OnRoomStartClient() { }

    /// <summary>
    /// 클라이언트가 중지될 때 호출됩니다.
    /// </summary>
    public override void OnRoomStopClient() { }

    /// <summary>
    /// 클라이언트가 새로운 네트워크 씬 로딩을 완료했을 때 호출됩니다.
    /// </summary>
    public override void OnRoomClientSceneChanged() { }

    #endregion

    #region 선택적 UI

    public override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion
    */
}