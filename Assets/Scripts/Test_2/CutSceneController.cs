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
            // 각각의 비디오 플레이어에 이벤트 등록. 연결해준다는 개념으로 생각하면 됨
            videoPlayer.started += DisablePlayerControl;
            videoPlayer.loopPointReached += EnablePlayerControl;
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        staminaController.enabled = false; // 스태미너 기능 비활성화
        staminaController.DisableStaminaUI(); // 스태미너 UI 비활성화
        playerController.enabled = false; // 컷씬 시작하면 플레이어 멈춤
        postProcessVolume.enabled = false; // PostProcess 비활성화

        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true; // 컷씬 끝나면 플레이어 이동 가능
        postProcessVolume.enabled = true; // PostProcess 활성화
        staminaController.enabled = true; // 스태미너 기능 다시 활성화
        staminaController.EnableStaminaUI(); // 스태미너 UI 다시 활성화

        Debug.Log("PostProcess Volume Enabled");
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + "video end");

        if (Scenechange&&!isMyproject)
        {
            changeTheScene.StartLoadingScene("myproject"); // 씬 전환
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
