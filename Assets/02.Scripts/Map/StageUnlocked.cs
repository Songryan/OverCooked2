using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StageUnlocked : MonoBehaviour
{
    public float scaleSpeed = 0.5f;
    public float minScale = 0.2f;
    public float midScale = 0.5f;
    public float maxScale = 1.0f;   
    public GameObject[] childObjects;

    void Start()
    {
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �ش� GameObject�� "Bus" �±װ� onenter�Ǹ�
        if (other.CompareTag("Bus"))
        {
            SetChildrenActive(true);
            foreach (GameObject childObject in childObjects)
            {
                //childObject.transform.localScale = Vector3.one;
                childObject.transform.localScale = new Vector3 (midScale, midScale, midScale);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �ش� GameObject�� "Bus" �±װ� exit�Ǹ�
        if (other.CompareTag("Bus"))
        {
            // �������� �ּ� ũ��� ����
            foreach (GameObject childObject in childObjects)
            {
                childObject.transform.localScale = Vector3.one * minScale;
            }

            // �ڽ� GameObject���� ��Ȱ��ȭ
            SetChildrenActive(false);
        }
    }

    // �ڽ� GameObject���� Ȱ��ȭ ���θ� �����ϴ� �Լ�
    void SetChildrenActive(bool active)
    {
        foreach (GameObject childObject in childObjects)
        {
            childObject.SetActive(active);
        }
    }
    IEnumerator ScaleDown()
    {
        while (childObjects[0].transform.localScale.magnitude > minScale)
        {
            // StageUI �±׸� ���� �ڽ� GameObject���� �������� ���
            foreach (GameObject stageUIObject in childObjects)
            {
                stageUIObject.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            }

            yield return null;
        }
    }

    IEnumerator ScaleUp()
    {
        // �ִ� ũ�⿡ ������ ������ �ݺ�
        while (childObjects[0].transform.localScale.magnitude < maxScale)
        {
            // StageUI �±׸� ���� �ڽ� GameObject���� �������� Ȯ��
            foreach (GameObject stageUIObject in childObjects)
            {
                stageUIObject.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }

            yield return null;
        }
    }



}