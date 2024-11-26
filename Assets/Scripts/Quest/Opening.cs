using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

public class Opening : MonoBehaviour
{
    public VideoPlayer OpeningVideo; // VideoPlayer 컴포넌트를 연결합니다.
    public PlayerMovement playerController;
    public PostProcessVolume postProcessVolume;
    public StaminaController staminaController;
    public BoxCollider[] boxColliders;
    public GameObject crosshair;

    void Start()
    {
        OpeningVideo.started += DisablePlayerControl;
        OpeningVideo.loopPointReached += EnablePlayerControl;
        OpeningVideo.Play();
        Debug.Log("Opening video play");

        foreach (BoxCollider B in boxColliders)
        {
            if (B.enabled)
            {
                B.enabled = false;  //박스들 비활성화
            }
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false; // 컷씬 시작하면 플레이어 비활성화
        postProcessVolume.enabled = false; // PostProcess 비활성화
        staminaController.enabled = false; // 스태미너 기능 비활성화
        staminaController.DisableStaminaUI(); // 스태미너 UI 비활성화
        crosshair.SetActive(false);

        Debug.Log("Player Control Disabled and Stamina UI Hidden");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true; // 컷씬 끝나면 플레이어 활성화
        OpeningVideo.enabled = false; // 비디오 비활성화
        postProcessVolume.enabled = true; // PostProcess 다시 활성화
        staminaController.enabled = true; // 스태미너 기능 다시 활성화
        staminaController.EnableStaminaUI(); // 스태미너 UI 다시 활성화
        crosshair?.SetActive(true);

        Debug.Log("Player Control Enabled and Stamina UI Shown");
    }
}
