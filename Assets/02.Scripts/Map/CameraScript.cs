using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject bus;
    public Transform target; // Ÿ�� ������Ʈ�� Transform

    public Vector3 offset; // ī�޶�� Ÿ�� ������ �Ÿ�

    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ�


    void Start()
    {
        BusFind();

    }

    // Update is called once per frame
    void Update()
    {
        BusFind();
        FlowBus();
    }

    void BusFind()
    {
        if (bus == null)
        {
            bus = GameObject.FindWithTag("Bus");
        }
        else
        {
            Debug.Log("Bus Find Fall!!");
        }
    }
    
    void FlowBus()
    {
       
        //gameObject.transform.position = new Vector3(bus.transform.position.x, gameObject.transform.position.y, bus.transform.position.z);

    }
}
