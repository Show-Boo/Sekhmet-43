using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepSource; // �߼Ҹ��� ����ϴ� AudioSource
    public AudioClip footstepClip; // �߼Ҹ� AudioClip
    public float stepInterval = 0.3f; // �߼Ҹ� ��� ����
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        stepTimer = stepInterval;
        characterController = GetComponent<CharacterController>();

        if (footstepSource == null)
        {
            Debug.LogError("AudioSource�� �������� �ʾҽ��ϴ�.");
        }

        if (characterController == null)
        {
            Debug.LogError("CharacterController�� �������� �ʾҽ��ϴ�.");
        }

        if (footstepClip == null)
        {
            Debug.LogError("FootstepClip�� �������� �ʾҽ��ϴ�.");
        }

        footstepSource.loop = false; // �߼Ҹ��� �ݺ� ������� �ʵ��� ����
    }

    void Update()
    {
        // �÷��̾ �����̴��� Ȯ��
        if (IsPlayerMoving())
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0; // �÷��̾ ���� ��� Ÿ�̸Ӹ� ����
        }
    }

    bool IsPlayerMoving()
    {
        // CharacterController�� �ӵ��� ����Ͽ� �÷��̾ �����̰� �ִ��� Ȯ��
        return characterController != null && characterController.velocity.magnitude > 0.1f;
    }

    void PlayFootstep()
    {
        Debug.Log("�߼Ҹ� ���");
        footstepSource.PlayOneShot(footstepClip);
    }
}
