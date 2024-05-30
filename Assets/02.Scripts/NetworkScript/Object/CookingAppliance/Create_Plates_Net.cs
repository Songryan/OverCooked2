using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Plates_Net : MonoBehaviour
{
    private void Start()
    {
        OverNetworkRoomManager manager = (OverNetworkRoomManager)NetworkRoomManager.singleton;
        GameObject Plate = Instantiate(manager.GetUIObject_RoomManager("Plate"));
        Plate.transform.SetParent(transform, false);
    }
}
