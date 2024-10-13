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

    public BoxCollider[] boxColliders;

    //public GameObject SceneChange;

    // Start is called before the first frame update
    void Start()
    {
        OpeningVideo.started += DisablePlayerControl;
        OpeningVideo.loopPointReached += EnablePlayerControl;
        OpeningVideo.Play();
        Debug.Log("opening video play");

        foreach (BoxCollider B in boxColliders)
        {
            if (B.enabled)
            {
                B.enabled = false;  //�ڽ��� ��Ȱ��ȭ
            }
        }
        //SceneChange.SetActive(false);//Ȱ��ȭ
    }

    // Update is called once per frame
    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false;//�ƾ� �����ϸ� player �����ֱ�
        postProcessVolume.enabled = false; // Volume ��ü�� ��Ȱ��ȭ
        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;//�ƾ� ������ player �̵� �����ϰ� ���ֱ�
        OpeningVideo.enabled = false;
        // �ƾ��� ������ �� PostProcessing Volume �ٽ� Ȱ��ȭ
        postProcessVolume.enabled = true; // Volume ��ü�� �ٽ� Ȱ��ȭ
        Debug.Log("PostProcess Volume Enabled");
    }
}

    
