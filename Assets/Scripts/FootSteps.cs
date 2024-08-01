using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepSource; // 발소리를 재생하는 AudioSource
    public AudioClip footstepClip; // 발소리 AudioClip
    public float walkStepInterval = 0.4f; // 걸을 때 발소리 재생 간격
    public float runStepInterval = 0.3f;// 뛸 때 발소리 재생 간격
    public float footstepVolume = 0.07f;
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        stepTimer = walkStepInterval;
        characterController = GetComponent<CharacterController>();

        if (footstepSource == null)
        {
            Debug.LogError("AudioSource가 설정되지 않았습니다.");
        }

        if (characterController == null)
        {
            Debug.LogError("CharacterController가 설정되지 않았습니다.");
        }

        if (footstepClip == null)
        {
            Debug.LogError("FootstepClip이 설정되지 않았습니다.");
        }

        footstepSource.loop = false;
        footstepSource.volume = footstepVolume;// 발소리를 반복 재생하지 않도록 설정
    }

    void Update()
    {
        // 플레이어가 움직이는지 확인
        if (IsPlayerMoving())
        {
            // Shift 키를 누르고 있으면 뛰는 것으로 간주
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
                // 현재 상태에 따라 발소리 간격을 설정
                stepTimer = Input.GetKey(KeyCode.LeftShift) ? runStepInterval : walkStepInterval;
            }
        }
        else
        {
            stepTimer = 0; // 플레이어가 멈춘 경우 타이머를 리셋
        }
    }

    bool IsPlayerMoving()
    {
        // CharacterController의 속도를 사용하여 플레이어가 움직이고 있는지 확인
        return characterController != null && characterController.velocity.magnitude > 0.1f;
    }

    void PlayFootstep()
    {
        Debug.Log("발소리 재생");
        footstepSource.PlayOneShot(footstepClip,footstepVolume);
    }
}
