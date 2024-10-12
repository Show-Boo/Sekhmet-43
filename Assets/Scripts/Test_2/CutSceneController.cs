using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers; // VideoPlayer 컴포넌트를 연결합니다.
    public PlayerMovement playerController; // PlayerMovement와 같은 플레이어 컨트롤러 스크립트를 연결합니다.
    public PostProcessVolume postProcessVolume;

    public int nowIndex = 0;
    public int previousIndex = 0;

    public bool Scenechange = false;
    public GameObject SceneChange;
    void Start()
    {
        foreach (var videoPlayer in videoPlayers)
        {
            // 각각의 비디오 플레이어에 이벤트 등록. 연결해준다는 개념으로 생각하면 됨
            videoPlayer.started += DisablePlayerControl;
            videoPlayer.loopPointReached += EnablePlayerControl;
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        //playerController.enabled = false;//컷씬 시작하면 player 멈춰주기
        postProcessVolume.enabled = false; // Volume 자체를 비활성화
        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        //playerController.enabled = true;//컷씬 끝나면 player 이동 가능하게 해주기
        
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + "video end");

        if (Scenechange)
        {
            SceneChange.SetActive(true);//활성화
        }
        // 컷씬이 끝났을 때 PostProcessing Volume 다시 활성화
        postProcessVolume.enabled = true; // Volume 자체를 다시 활성화
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
