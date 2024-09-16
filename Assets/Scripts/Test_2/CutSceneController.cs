using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer ������Ʈ�� �����մϴ�.
    public PlayerMovement playerController; // PlayerMovement�� ���� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� �����մϴ�.


    public int nowIndex = 0;
    public int previousIndex = 0;
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
        playerController.enabled = false;//�ƾ� �����ϸ� player �����ֱ�
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;//�ƾ� ������ player �̵� �����ϰ� ���ֱ�
        
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + "video end");
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
