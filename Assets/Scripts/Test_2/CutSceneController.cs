using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer ������Ʈ�� �����մϴ�.
    public PlayerMovement playerController; // PlayerMovement�� ���� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� �����մϴ�.


    public int nowIndex;
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
        videoPlayers[nowIndex].enabled = false;
    }

    public void PlayCutscene(int index)
    {
        if (index >= 0 && index < videoPlayers.Length)
        {
            videoPlayers[index].Play();
            Debug.Log("video play");

            nowIndex = index;

        }
    }
}
