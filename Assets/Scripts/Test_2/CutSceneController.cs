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

    public int nowIndex = 0;
    public int previousIndex = 0;

    public bool Scenechange = false;
    public GameObject SceneChange;
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
        //playerController.enabled = false;//�ƾ� �����ϸ� player �����ֱ�
        postProcessVolume.enabled = false; // Volume ��ü�� ��Ȱ��ȭ
        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        //playerController.enabled = true;//�ƾ� ������ player �̵� �����ϰ� ���ֱ�
        
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + "video end");

        if (Scenechange)
        {
            SceneChange.SetActive(true);//Ȱ��ȭ
        }
        // �ƾ��� ������ �� PostProcessing Volume �ٽ� Ȱ��ȭ
        postProcessVolume.enabled = true; // Volume ��ü�� �ٽ� Ȱ��ȭ
        Debug.Log("PostProcess Volume Enabled");

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
