using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    [SerializeField] AudioClip flashlightOnSound;  // 플래시라이트 켤 때 효과음
    [SerializeField] AudioClip flashlightOffSound; // 플래시라이트 끌 때 효과음
    private AudioSource audioSource;               // AudioSource 컴포넌트
    private bool FlashlightActive = false;

    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);

        // AudioSource 컴포넌트를 가져옴
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource가 없습니다. 컴포넌트를 추가하세요.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashlightActive == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                PlaySound(flashlightOnSound);   // 켤 때 효과음 재생
                FlashlightActive = true;
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                PlaySound(flashlightOffSound);  // 끌 때 효과음 재생
                FlashlightActive = false;
            }
        }
    }

    // 효과음을 재생하는 함수
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
