using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private GameObject[] BGMSquares; //���� �׸�׸�
    [SerializeField] private GameObject[] effectSquares;



    [Header("����")]
    public float volumeBGM = 0.2f;
    public float volumeEffect = 1f;

    [Header("�������")]
    public AudioSource BgmAudioSource;

    [Header("ȿ����")]
    public AudioSource EffectAudioSource;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void upBgmSound()
    {
        volumeBGM += 0.1f;
        if (volumeBGM >= 1)
        {
            volumeBGM = 1f;
        }
        //BgmAudioSource.volume = volumeBGM;
        SetBGMSquares();
    }
    public void downBgmSound()
    {
        volumeBGM -= 0.1f;
        if (volumeBGM <= 0)
        {
            volumeBGM = 0;
        }
        //BgmAudioSource.volume = volumeBGM;
        SetBGMSquares();
    }

    private void SetBGMSquares()
    {
        Debug.Log(volumeBGM);
        for (int i = 0; i < BGMSquares.Length; i++)
        {
            if (i <volumeBGM * 10) // ���� ������ ���� Ȱ��ȭ�� �׸���� ������ �����մϴ�.
            {
                BGMSquares[i].transform.GetChild(0).gameObject.SetActive(false);
                BGMSquares[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else // ������ �׸���� ��Ȱ��ȭ�մϴ�.
            {
                BGMSquares[i].transform.GetChild(0).gameObject.SetActive(true);
                BGMSquares[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void upEffectSound()
    {
        volumeEffect += 0.1f;
        if (volumeEffect >= 1)
        {
            volumeEffect = 1f;
        }
        //EffectAudioSource.volume = volumeEffect;
        SetEffectSquares();
    }
    public void downEffectSound()
    {
        volumeEffect -= 0.1f;
        if (volumeEffect <= 0)
        {
            volumeEffect = 0;
        }
        //EffectAudioSource.volume = volumeEffect;
        SetEffectSquares();
    }

    private void SetEffectSquares()
    {
        int j = 0;
        for (float i = 0; i < 1; i += 0.1f)
        {
            if (i < volumeEffect) 
            {
                effectSquares[j].transform.GetChild(0).gameObject.SetActive(false);
                effectSquares[j].transform.GetChild(1).gameObject.SetActive(true);
            }
            else 
            {
                effectSquares[j].transform.GetChild(0).gameObject.SetActive(true);
                effectSquares[j].transform.GetChild(1).gameObject.SetActive(false);
            }
            j++;
        }
    }
}
