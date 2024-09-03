using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer ������Ʈ�� �����մϴ�.
    public MonoBehaviour playerController; // PlayerMovement�� ���� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� �����մϴ�.

    void Start()
    {
        foreach (var videoPlayer in videoPlayers)
        {
            // ������ ���� �÷��̾ �̺�Ʈ ���
            videoPlayer.started += DisablePlayerControl;
            videoPlayer.loopPointReached += EnablePlayerControl;
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false;
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;
    }

    public void PlayCutscene(int index)
    {
        if (index >= 0 && index < videoPlayers.Length)
        {
            videoPlayers[index].Play();
        }
    }
}
