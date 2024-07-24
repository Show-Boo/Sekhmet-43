using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstep;
    public AudioClip footstepClip; // 발소리 오디오 클립
    public float stepInterval = 0.5f; // 발소리 재생 간격
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        stepTimer = stepInterval;
        characterController = GetComponent<CharacterController>();

        if (footstep == null)
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
    }

    void Update()
    {
        Debug.Log("Velocity: " + characterController.velocity.magnitude); // 플레이어 속도 확인용 로그

        // 플레이어가 움직이는지 확인
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
            // 플레이어가 멈춘 경우 타이머를 리셋하고 발소리 중지
            stepTimer = stepInterval;
            StopFootstep();
        }
    }

    bool IsPlayerMoving()
    {
        // CharacterController의 속도를 사용하여 플레이어가 움직이고 있는지 확인
        return characterController != null && characterController.velocity.magnitude > 0.1f;
    }

    void PlayFootstep()
    {
        if (!footstep.isPlaying)
        {
            Debug.Log("발소리 재생");
            footstep.PlayOneShot(footstepClip);
        }
    }

    void StopFootstep()
    {
        if (footstep.isPlaying)
        {
            Debug.Log("발소리 중지");
            footstep.Stop();
        }
    }
}
