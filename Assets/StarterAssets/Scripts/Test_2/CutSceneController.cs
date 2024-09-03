using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer 컴포넌트를 연결합니다.
    public MonoBehaviour playerController; // PlayerMovement와 같은 플레이어 컨트롤러 스크립트를 연결합니다.

    void Start()
    {
        foreach (var videoPlayer in videoPlayers)
        {
            // 각각의 비디오 플레이어에 이벤트 등록
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
