using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float holdTime = 3.0f;         // R Ű�� ������ �־�� �ϴ� �ð�
    private bool isHoldingR = false;      // R Ű�� ���� �ִ��� ����
    private float currentHoldTime = 0;
    public GameObject deadCutScene;

    void Update()
    {
        // �÷��̾� �̵� ó��
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // R Ű�� ������ ������ ��
        if (Input.GetKeyDown(KeyCode.R))
        {
            isHoldingR = true;
            currentHoldTime = 0;
        }

        // R Ű�� ������ ��
        if (Input.GetKeyUp(KeyCode.R))
        {
            isHoldingR = false;
            currentHoldTime = 0;
        }

        // R Ű�� Ȧ�� ���̶�� �ð� üũ
        if (isHoldingR)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdTime)
            {
                // CutScene Ȱ��ȭ
                deadCutScene.SetActive(true);

                // ��� AudioSource�� ���Ұ�
                AudioSource[] cutSceneAudios = deadCutScene.GetComponentsInChildren<AudioSource>();
                foreach (AudioSource audio in cutSceneAudios)
                {
                    audio.volume = 0; // ������ 0���� �����Ͽ� ���Ұ�
                }

                // VideoPlayer ���Ұ� ����
                VideoPlayer videoPlayer = deadCutScene.GetComponentInChildren<VideoPlayer>();
                if (videoPlayer != null)
                {
                    videoPlayer.SetDirectAudioMute(0, true); // 0�� �⺻ ����� Ʈ��
                }

                isHoldingR = false;
            }
        }
    }
}
