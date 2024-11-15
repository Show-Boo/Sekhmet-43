using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;

public class DeadCutScene : MonoBehaviour
{
    public CheckPoint checkPoint;
    public PlayerMovement playerController;
    public StaminaController staminaController;
    public GameObject blackOut;

    void Start()
    {
        // VideoPlayer 컴포넌트 가져오기
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        // 비디오 시작과 끝에 대한 함수 등록
        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;
    }

    void StartControl(VideoPlayer vp)
    {
        // 스태미나 UI 비활성화
        staminaController.DisableForCutscene();

        // 플레이어 컨트롤 비활성화
        playerController.enabled = false;
    }

    void EndControl(VideoPlayer vp)
    {
        // 스태미나 UI 활성화
        staminaController.EnableAfterCutscene();

        // 플레이어 컨트롤 다시 활성화
        playerController.enabled = true;

        // 체크포인트 리스타트
        checkPoint.restart = true;

        Debug.Log("checkpoint");

        // 검은 화면 활성화
        blackOut.SetActive(true);

        // 컷씬 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
