using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;

public class DeadCutScene : MonoBehaviour
{
    public CheckPoint checkPoint;
    public PlayerMovement playerController;
    public StaminaController staminaController;
    public GameObject blackOut;

    void Start()
    {
        // VideoPlayer ������Ʈ ��������
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        // ���� ���۰� ���� ���� �Լ� ���
        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;
    }

    void StartControl(VideoPlayer vp)
    {
        // ���¹̳� UI ��Ȱ��ȭ
        staminaController.DisableForCutscene();

        // �÷��̾� ��Ʈ�� ��Ȱ��ȭ
        playerController.enabled = false;
    }

    void EndControl(VideoPlayer vp)
    {
        // ���¹̳� UI Ȱ��ȭ
        staminaController.EnableAfterCutscene();

        // �÷��̾� ��Ʈ�� �ٽ� Ȱ��ȭ
        playerController.enabled = true;

        // üũ����Ʈ ����ŸƮ
        checkPoint.restart = true;

        Debug.Log("checkpoint");

        // ���� ȭ�� Ȱ��ȭ
        blackOut.SetActive(true);

        // �ƾ� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
