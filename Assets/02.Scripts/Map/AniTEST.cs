using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTEST : MonoBehaviour
{
    
    // �ڽ� ��ü�� �ִϸ����� ������Ʈ�� ������ �迭
    private Animator[] childAnimators;

    void Start()
    {
        // ��� �ڽ� ��ü�� �ִϸ����� ������Ʈ�� ������
        childAnimators = GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        // Ư�� ������ �����ϸ� ��� �ڽ� ��ü�� �ִϸ��̼��� �����Ŵ
        if (Input.GetKeyDown(KeyCode.Space)) // ���÷� �����̽��ٸ� ���� �� �����ϵ��� ����
        {
            PlayChildAnimations();
        }
    }

    void PlayChildAnimations()
    {
      
            foreach (Animator animator in childAnimators)
            {
                animator.SetTrigger("flip");
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bus"))
        {
            foreach (Animator animator in childAnimators)
            {
                animator.SetTrigger("flip");
            }
        }
    }
}
