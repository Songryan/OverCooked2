using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePcOptionSet : MonoBehaviour
{
    public GameObject pcOption;
    public GameObject mobileOption;

    public void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            pcOption.SetActive(false);
            mobileOption.SetActive(true);
        }
        else
        {
            pcOption.SetActive(true);
            mobileOption.SetActive(false);
        }

    }
}
