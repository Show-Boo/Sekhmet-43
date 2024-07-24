using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstep;
    public AudioClip footstepClip; // �߼Ҹ� ����� Ŭ��
    public float stepInterval = 0.5f; // �߼Ҹ� ��� ����
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        stepTimer = stepInterval;
        characterController = GetComponent<CharacterController>();

        if (footstep == null)
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
    }

    void Update()
    {
        Debug.Log("Velocity: " + characterController.velocity.magnitude); // �÷��̾� �ӵ� Ȯ�ο� �α�

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
            // �÷��̾ ���� ��� Ÿ�̸Ӹ� �����ϰ� �߼Ҹ� ����
            stepTimer = stepInterval;
            StopFootstep();
        }
    }

    bool IsPlayerMoving()
    {
        // CharacterController�� �ӵ��� ����Ͽ� �÷��̾ �����̰� �ִ��� Ȯ��
        return characterController != null && characterController.velocity.magnitude > 0.1f;
    }

    void PlayFootstep()
    {
        if (!footstep.isPlaying)
        {
            Debug.Log("�߼Ҹ� ���");
            footstep.PlayOneShot(footstepClip);
        }
    }

    void StopFootstep()
    {
        if (footstep.isPlaying)
        {
            Debug.Log("�߼Ҹ� ����");
            footstep.Stop();
        }
    }
}
