using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCallNetworkManager : MonoBehaviour
{
    OverNetworkRoomManager overNetworkRoomManager;
    UIManager uIManager;

    private void Start()
    {
        overNetworkRoomManager = FindObjectOfType<OverNetworkRoomManager>();
    }

    public void ExitGameRoom()
    {
        overNetworkRoomManager = FindObjectOfType<OverNetworkRoomManager>();
        overNetworkRoomManager.ReturnToRoomScene();
    }
}
