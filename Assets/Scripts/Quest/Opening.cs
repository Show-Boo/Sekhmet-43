using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

public class Opening : MonoBehaviour
{
    public VideoPlayer OpeningVideo; // VideoPlayer ������Ʈ�� �����մϴ�.
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
                B.enabled = false;  //�ڽ��� ��Ȱ��ȭ
            }
        }
    }

    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false; // �ƾ� �����ϸ� �÷��̾� ��Ȱ��ȭ
        postProcessVolume.enabled = false; // PostProcess ��Ȱ��ȭ
        staminaController.enabled = false; // ���¹̳� ��� ��Ȱ��ȭ
        staminaController.DisableStaminaUI(); // ���¹̳� UI ��Ȱ��ȭ
        crosshair.SetActive(false);

        Debug.Log("Player Control Disabled and Stamina UI Hidden");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true; // �ƾ� ������ �÷��̾� Ȱ��ȭ
        OpeningVideo.enabled = false; // ���� ��Ȱ��ȭ
        postProcessVolume.enabled = true; // PostProcess �ٽ� Ȱ��ȭ
        staminaController.enabled = true; // ���¹̳� ��� �ٽ� Ȱ��ȭ
        staminaController.EnableStaminaUI(); // ���¹̳� UI �ٽ� Ȱ��ȭ
        crosshair?.SetActive(true);

        Debug.Log("Player Control Enabled and Stamina UI Shown");
    }
}
