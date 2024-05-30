using UnityEngine;
using Mirror;

/*
    문서: https://mirror-networking.gitbook.io/docs/components/network-room-player
    API 참조: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// 이 컴포넌트는 NetworkRoomManager와 함께 멀티플레이어 룸 시스템을 구성합니다.
/// NetworkRoomManager의 RoomPrefab 오브젝트는 이 컴포넌트를 가지고 있어야 합니다.
/// 이 컴포넌트는 룸이 정상적으로 동작하는 데 필요한 기본적인 룸 플레이어 데이터를 보유합니다.
/// 게임 특정 데이터는 RoomPrefab의 다른 컴포넌트나 NetworkRoomPlayer를 상속받는 스크립트에서 관리할 수 있습니다.
/// </summary>
public class OverNetworkRoomPlayer : NetworkRoomPlayer
{
    

    /*
    #region Start & Stop 콜백

    /// <summary>
    /// NetworkBehaviour 객체가 서버에서 활성화될 때 호출됩니다.
    /// <para>이것은 NetworkServer.Listen()에 의해 씬에 있는 객체나 NetworkServer.Spawn()에 의해 동적으로 생성된 객체에 의해 트리거될 수 있습니다.</para>
    /// <para>호스트와 전용 서버 모두에서 이 함수가 호출됩니다.</para>
    /// </summary>
    public override void OnStartServer() { }

    /// <summary>
    /// 객체가 언스폰될 때 서버에서 호출됩니다.
    /// <para>객체 데이터를 영구 저장소에 저장하는 데 유용합니다.</para>
    /// </summary>
    public override void OnStopServer() { }

    /// <summary>
    /// NetworkBehaviour가 클라이언트에서 활성화될 때 호출됩니다.
    /// <para>호스트의 객체는 로컬 클라이언트가 있기 때문에 이 함수가 호출됩니다. SyncVars의 값은 이 함수가 클라이언트에서 호출될 때 최신 상태로 초기화됩니다.</para>
    /// </summary>
    public override void OnStartClient() { }

    /// <summary>
    /// 서버가 이 객체를 파괴할 때 클라이언트에서 호출됩니다.
    /// <para>이 함수는 이펙트를 호출하거나 클라이언트 특정 정리를 하는 데 사용할 수 있습니다.</para>
    /// </summary>
    public override void OnStopClient() { }

    /// <summary>
    /// 로컬 플레이어 객체가 설정되었을 때 호출됩니다.
    /// <para>이 함수는 OnStartClient() 이후에 호출되며, 서버로부터 소유권 메시지가 전달될 때 트리거됩니다. 카메라나 입력과 같은 로컬 플레이어에게만 활성화해야 하는 기능을 활성화하는 데 적절한 장소입니다.</para>
    /// </summary>
    public override void OnStartLocalPlayer() { }

    /// <summary>
    /// 권한을 가진 객체에서 호출됩니다. 
    /// <para>이 함수는 OnStartServer 이후, OnStartClient 이전에 호출됩니다.</para>
    /// <para>NetworkIdentity.AssignClientAuthority가 서버에서 호출될 때, 이 함수는 객체를 소유한 클라이언트에서 호출됩니다. NetworkServer.Spawn에서 NetworkConnectionToClient 매개변수가 포함된 객체가 소환될 때, 이 함수는 객체를 소유한 클라이언트에서 호출됩니다.</para>
    /// </summary>
    public override void OnStartAuthority() { }

    /// <summary>
    /// 권한이 제거될 때 호출됩니다.
    /// <para>NetworkIdentity.RemoveClientAuthority가 서버에서 호출될 때, 이 함수는 객체를 소유한 클라이언트에서 호출됩니다.</para>
    /// </summary>
    public override void OnStopAuthority() { }

    #endregion

    #region 룸 클라이언트 콜백

    /// <summary>
    /// 룸에 들어갈 때 모든 플레이어 객체에서 호출되는 후크입니다.
    /// <para>참고: isLocalPlayer는 OnStartLocalPlayer가 호출될 때까지 설정되지 않을 수 있습니다.</para>
    /// </summary>
    public override void OnClientEnterRoom() { }

    /// <summary>
    /// 룸을 나갈 때 모든 플레이어 객체에서 호출되는 후크입니다.
    /// </summary>
    public override void OnClientExitRoom() { }

    #endregion

    #region SyncVar 후크

    /// <summary>
    /// 인덱스가 변경될 때 클라이언트에서 호출되는 후크입니다.
    /// </summary>
    /// <param name="oldIndex">이전 인덱스 값</param>
    /// <param name="newIndex">새 인덱스 값</param>
    public override void IndexChanged(int oldIndex, int newIndex) { }

    /// <summary>
    /// RoomPlayer가 준비 상태로 전환될 때 클라이언트에서 호출되는 후크입니다.
    /// <para>이 함수는 클라이언트 플레이어가 SendReadyToBeginMessage()나 SendNotReadyToBeginMessage()를 호출할 때 실행됩니다.</para>
    /// </summary>
    /// <param name="oldReadyState">이전 준비 상태 값</param>
    /// <param name="newReadyState">새 준비 상태 값</param>
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) { }

    #endregion

    #region 선택적 UI

    public override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion
    */
}
