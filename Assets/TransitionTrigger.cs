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
        staminaController.DisableForCutscene(); // �ƾ� �� ���¹̳� UI ��Ȱ��ȭ
        playerController.enabled = false;
        postProcessVolume.enabled = false;

    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;
        postProcessVolume.enabled = true;
        staminaController.EnableAfterCutscene(); // �ƾ� ���� �� ���¹̳� UI Ȱ��ȭ

        videoPlayer.enabled = false;
    }


    // Ʈ���� �ڽ��� ������ �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (other.CompareTag("Player"))
        {
            // �ƾ� ���
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }

            // Ʈ���� �ڽ� ��Ȱ��ȭ (���ϴ� ���)
            gameObject.SetActive(false);
        }
    }
}

