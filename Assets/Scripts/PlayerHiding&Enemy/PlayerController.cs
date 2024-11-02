using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float holdTime = 3.0f;         // R 키를 누르고 있어야 하는 시간
    private bool isHoldingR = false;      // R 키가 눌려 있는지 여부
    private float currentHoldTime = 0;
    public GameObject deadCutScene;

    void Update()
    {
        // 플레이어 이동 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // R 키를 누르기 시작할 때
        if (Input.GetKeyDown(KeyCode.R))
        {
            isHoldingR = true;
            currentHoldTime = 0;
        }

        // R 키를 떼었을 때
        if (Input.GetKeyUp(KeyCode.R))
        {
            isHoldingR = false;
            currentHoldTime = 0;
        }

        // R 키를 홀드 중이라면 시간 체크
        if (isHoldingR)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdTime)
            {
                // CutScene 활성화
                deadCutScene.SetActive(true);

                // 모든 AudioSource를 음소거
                AudioSource[] cutSceneAudios = deadCutScene.GetComponentsInChildren<AudioSource>();
                foreach (AudioSource audio in cutSceneAudios)
                {
                    audio.volume = 0; // 볼륨을 0으로 설정하여 음소거
                }

                // VideoPlayer 음소거 설정
                VideoPlayer videoPlayer = deadCutScene.GetComponentInChildren<VideoPlayer>();
                if (videoPlayer != null)
                {
                    videoPlayer.SetDirectAudioMute(0, true); // 0은 기본 오디오 트랙
                }

                isHoldingR = false;
            }
        }
    }
}
