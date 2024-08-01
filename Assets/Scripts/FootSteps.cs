using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepSource; // �߼Ҹ��� ����ϴ� AudioSource
    public AudioClip footstepClip; // �߼Ҹ� AudioClip
    public float walkStepInterval = 0.4f; // ���� �� �߼Ҹ� ��� ����
    public float runStepInterval = 0.3f;// �� �� �߼Ҹ� ��� ����
    public float footstepVolume = 0.07f;
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        stepTimer = walkStepInterval;
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

        footstepSource.loop = false;
        footstepSource.volume = footstepVolume;// �߼Ҹ��� �ݺ� ������� �ʵ��� ����
    }

    void Update()
    {
        // �÷��̾ �����̴��� Ȯ��
        if (IsPlayerMoving())
        {
            // Shift Ű�� ������ ������ �ٴ� ������ ����
            if (Input.GetKey(KeyCode.LeftShift))
            {
                stepTimer -= Time.deltaTime * (walkStepInterval / runStepInterval);
            }
            else
            {
                stepTimer -= Time.deltaTime;
            }

            if (stepTimer <= 0)
            {
                PlayFootstep();
                // ���� ���¿� ���� �߼Ҹ� ������ ����
                stepTimer = Input.GetKey(KeyCode.LeftShift) ? runStepInterval : walkStepInterval;
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
        footstepSource.PlayOneShot(footstepClip,footstepVolume);
    }
}
