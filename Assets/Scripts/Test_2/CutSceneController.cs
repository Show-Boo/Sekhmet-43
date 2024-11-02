using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer ������Ʈ�� �����մϴ�.
    public PlayerMovement playerController; // PlayerMovement�� ���� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� �����մϴ�.
    public PostProcessVolume postProcessVolume;
    public StaminaController staminaController;

    public bool isMyproject=false;


    public int nowIndex = 0;
    public int previousIndex = 0;

    public bool Scenechange = false;
    public ChangeTheScene changeTheScene;
    void Start()
    {
        foreach (var videoPlayer in videoPlayers)
        {
            // ������ ���� �÷��̾ �̺�Ʈ ���. �������شٴ� �������� �����ϸ� ��
            videoPlayer.started += DisablePlayerControl;
            videoPlayer.loopPointReached += EnablePlayerControl;
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        staminaController.enabled = false; // ���¹̳� ��� ��Ȱ��ȭ
        staminaController.DisableStaminaUI(); // ���¹̳� UI ��Ȱ��ȭ
        playerController.enabled = false; // �ƾ� �����ϸ� �÷��̾� ����
        postProcessVolume.enabled = false; // PostProcess ��Ȱ��ȭ

        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true; // �ƾ� ������ �÷��̾� �̵� ����
        postProcessVolume.enabled = true; // PostProcess Ȱ��ȭ
        staminaController.enabled = true; // ���¹̳� ��� �ٽ� Ȱ��ȭ
        staminaController.EnableStaminaUI(); // ���¹̳� UI �ٽ� Ȱ��ȭ

        Debug.Log("PostProcess Volume Enabled");
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + "video end");

        if (Scenechange&&!isMyproject)
        {
            changeTheScene.StartLoadingScene("myproject"); // �� ��ȯ
        }
    }

    public void PlayCutscene()
    {

        if (nowIndex >= 0 && nowIndex < videoPlayers.Length)
        {
            videoPlayers[nowIndex].Play();
            Debug.Log("video play");
            previousIndex = nowIndex;

            nowIndex++;

        }
    }

    
}
