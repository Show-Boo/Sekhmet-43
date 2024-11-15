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
        staminaController.DisableForCutscene(); // �ƾ� �� ���¹̳� UI ��Ȱ��ȭ
        playerController.enabled = false;
        postProcessVolume.enabled = false;
        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = true;//���ݸ���
            
        }

        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;
        postProcessVolume.enabled = true;
        staminaController.EnableAfterCutscene(); // �ƾ� ���� �� ���¹̳� UI Ȱ��ȭ

        Debug.Log("PostProcess Volume Enabled");
        videoPlayers[previousIndex].enabled = false;

        Debug.Log(previousIndex + " video end");

        if (Scenechange && !isMyproject)
        {
            changeTheScene.StartLoadingScene("myproject");
        }

        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = false;//�ٽ� ���� �簳

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
