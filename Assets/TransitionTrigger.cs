using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

public class TriggerCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PlayerMovement playerController;
    public PostProcessVolume postProcessVolume;
    public StaminaController staminaController;

    void Start()
    {
        videoPlayer.started += DisablePlayerControl;
        videoPlayer.loopPointReached += EnablePlayerControl;
       
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        staminaController.DisableForCutscene(); // 컷씬 중 스태미너 UI 비활성화
        playerController.enabled = false;
        postProcessVolume.enabled = false;

    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;
        postProcessVolume.enabled = true;
        staminaController.EnableAfterCutscene(); // 컷씬 종료 후 스태미너 UI 활성화

        videoPlayer.enabled = false;
    }


    // 트리거 박스에 들어왔을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // 컷씬 재생
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }

            // 트리거 박스 비활성화 (원하는 경우)
            gameObject.SetActive(false);
        }
    }
}

