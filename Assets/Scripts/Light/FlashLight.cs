using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    [SerializeField] AudioClip flashlightOnSound;  // �÷��ö���Ʈ �� �� ȿ����
    [SerializeField] AudioClip flashlightOffSound; // �÷��ö���Ʈ �� �� ȿ����
    private AudioSource audioSource;               // AudioSource ������Ʈ
    private bool FlashlightActive = false;

    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);

        // AudioSource ������Ʈ�� ������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource�� �����ϴ�. ������Ʈ�� �߰��ϼ���.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashlightActive == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                PlaySound(flashlightOnSound);   // �� �� ȿ���� ���
                FlashlightActive = true;
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                PlaySound(flashlightOffSound);  // �� �� ȿ���� ���
                FlashlightActive = false;
            }
        }
    }

    // ȿ������ ����ϴ� �Լ�
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
