using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerBreath : MonoBehaviour
{
    public Transform cameraTransform; // �÷��̾� ī�޶��� Ʈ������
    public AudioClip breathingSound; // ���Ҹ� ����� Ŭ��
    public float shakeAmount = 0.1f; // ī�޶� ��鸲 ����
    public float shakeSpeed = 1.0f; // ī�޶� ��鸲 �ӵ�
    public float breathingInterval = 3.0f; // ���Ҹ� ��� ���� (��)

    private AudioSource audioSource;
    private Vector3 initialCameraPosition;
    private float shakeTimer;
    private float breathingTimer;

    void Start()
    {
        // AudioSource ����
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // �ʱ� ī�޶� ��ġ ����
        initialCameraPosition = cameraTransform.localPosition;

        // Ÿ�̸� �ʱ�ȭ
        shakeTimer = 0f;
        breathingTimer = 0f;

        // ���Ҹ� ����� Ŭ���� �Ҵ���� ���� ��� ��� �޽��� ���
        if (breathingSound == null)
        {
            Debug.LogError("Breathing sound is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // ī�޶� ��鸲 ȿ��
        shakeTimer += Time.deltaTime * shakeSpeed;
        cameraTransform.localPosition = initialCameraPosition + new Vector3(0, Mathf.Sin(shakeTimer) * shakeAmount, 0);

        // ���Ҹ� ���
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
    public Transform cameraTransform; // �÷��̾� ī�޶��� Ʈ������
    public AudioClip breathingSound; // ���Ҹ� ����� Ŭ��
    public float shakeAmount = 0.1f; // ī�޶� ��鸲 ����
    public float shakeSpeed = 1.0f; // ī�޶� ��鸲 �ӵ�
    public float breathingInterval = 3.0f; // ���Ҹ� ��� ���� (��)
    public float breathingVolume = 1.0f; // ���Ҹ� ����

    private AudioSource breathingSource;
    private Vector3 initialCameraPosition;
    private float shakeTimer;
    private float breathingTimer;

    void Start()
    {
        // ���Ҹ� AudioSource ����
        breathingSource = gameObject.AddComponent<AudioSource>();
        breathingSource.clip = breathingSound;
        breathingSource.loop = false;
        breathingSource.volume = breathingVolume;

        // �ʱ� ī�޶� ��ġ ����
        initialCameraPosition = cameraTransform.localPosition;

        // Ÿ�̸� �ʱ�ȭ
        shakeTimer = 0f;
        breathingTimer = 0f;

        // ���Ҹ� ����� Ŭ���� �Ҵ���� ���� ��� ��� �޽��� ���
        if (breathingSound == null)
        {
            //Debug.LogError("Breathing sound is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // ī�޶� ��鸲 ȿ��
        shakeTimer += Time.deltaTime * shakeSpeed;
        cameraTransform.localPosition = initialCameraPosition + new Vector3(0, Mathf.Sin(shakeTimer) * shakeAmount, 0);

        // ���Ҹ� ���
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
