using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    public PlayerMovement playerController;
    public PostProcessVolume postProcessVolume;
    public StaminaController staminaController;

    public bool isMyproject = false;
    public int nowIndex = 0;
    public int previousIndex = 0;
    public bool Scenechange = false;
    public ChangeTheScene changeTheScene;

    public EnemyMove[] EnemyMove;

    void Start()
    {
        foreach (var videoPlayer in videoPlayers)
        {
            videoPlayer.started += DisablePlayerControl;
            videoPlayer.loopPointReached += EnablePlayerControl;
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        staminaController.DisableForCutscene(); // 컷씬 중 스태미너 UI 비활성화
        playerController.enabled = false;
        postProcessVolume.enabled = false;
        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = true;//공격멈춤
            
        }

        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;
        postProcessVolume.enabled = true;
        staminaController.EnableAfterCutscene(); // 컷씬 종료 후 스태미너 UI 활성화

        Debug.Log("PostProcess Volume Enabled");
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + " video end");

        if (Scenechange && !isMyproject)
        {
            changeTheScene.StartLoadingScene("myproject");
        }

        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = false;//다시 공격 재개

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
