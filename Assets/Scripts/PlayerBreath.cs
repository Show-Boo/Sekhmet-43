using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerBreath : MonoBehaviour
{
    public Transform cameraTransform; // 플레이어 카메라의 트랜스폼
    public AudioClip breathingSound; // 숨소리 오디오 클립
    public float shakeAmount = 0.1f; // 카메라 흔들림 정도
    public float shakeSpeed = 1.0f; // 카메라 흔들림 속도
    public float breathingInterval = 3.0f; // 숨소리 재생 간격 (초)

    private AudioSource audioSource;
    private Vector3 initialCameraPosition;
    private float shakeTimer;
    private float breathingTimer;

    void Start()
    {
        // AudioSource 설정
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 초기 카메라 위치 저장
        initialCameraPosition = cameraTransform.localPosition;

        // 타이머 초기화
        shakeTimer = 0f;
        breathingTimer = 0f;

        // 숨소리 오디오 클립이 할당되지 않은 경우 경고 메시지 출력
        if (breathingSound == null)
        {
            Debug.LogError("Breathing sound is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // 카메라 흔들림 효과
        shakeTimer += Time.deltaTime * shakeSpeed;
        cameraTransform.localPosition = initialCameraPosition + new Vector3(0, Mathf.Sin(shakeTimer) * shakeAmount, 0);

        // 숨소리 재생
        breathingTimer += Time.deltaTime;
        if (breathingTimer >= breathingInterval)
        {
            PlayBreathingSound();
            breathingTimer = 0f;
        }
    }

    void PlayBreathingSound()
    {
        if (breathingSound != null)
        {
            Debug.Log("Playing breathing sound");
            Debug.Log("AudioSource Volume: " + audioSource.volume);
            Debug.Log("AudioSource isMuted: " + audioSource.mute);
            audioSource.PlayOneShot(breathingSound);
        }
        else
        {
            Debug.LogError("Breathing sound is not assigned!");
        }
    }
}

*/

public class PlayerBreath : MonoBehaviour
{
    public Transform cameraTransform; // 플레이어 카메라의 트랜스폼
    public AudioClip breathingSound; // 숨소리 오디오 클립
    public float shakeAmount = 0.1f; // 카메라 흔들림 정도
    public float shakeSpeed = 1.0f; // 카메라 흔들림 속도
    public float breathingInterval = 3.0f; // 숨소리 재생 간격 (초)
    public float breathingVolume = 1.0f; // 숨소리 볼륨

    private AudioSource breathingSource;
    private Vector3 initialCameraPosition;
    private float shakeTimer;
    private float breathingTimer;

    void Start()
    {
        // 숨소리 AudioSource 설정
        breathingSource = gameObject.AddComponent<AudioSource>();
        breathingSource.clip = breathingSound;
        breathingSource.loop = false;
        breathingSource.volume = breathingVolume;

        // 초기 카메라 위치 저장
        initialCameraPosition = cameraTransform.localPosition;

        // 타이머 초기화
        shakeTimer = 0f;
        breathingTimer = 0f;

        // 숨소리 오디오 클립이 할당되지 않은 경우 경고 메시지 출력
        if (breathingSound == null)
        {
            //Debug.LogError("Breathing sound is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // 카메라 흔들림 효과
        shakeTimer += Time.deltaTime * shakeSpeed;
        cameraTransform.localPosition = initialCameraPosition + new Vector3(0, Mathf.Sin(shakeTimer) * shakeAmount, 0);

        // 숨소리 재생
        breathingTimer += Time.deltaTime;
        if (breathingTimer >= breathingInterval)
        {
            PlayBreathingSound();
            breathingTimer = 0f;
        }
    }

    void PlayBreathingSound()
    {
        if (breathingSound != null)
        {

            breathingSource.PlayOneShot(breathingSound, breathingVolume);
        }
        else
        {
            //Debug.LogError("Breathing sound is not assigned!");
        }
    }
}
