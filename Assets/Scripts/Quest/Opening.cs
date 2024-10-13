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
                B.enabled = false;  //박스들 비활성화
            }
        }
        //SceneChange.SetActive(false);//활성화
    }

    // Update is called once per frame
    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false;//컷씬 시작하면 player 멈춰주기
        postProcessVolume.enabled = false; // Volume 자체를 비활성화
        Debug.Log("PostProcess Volume Disabled");
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;//컷씬 끝나면 player 이동 가능하게 해주기
        OpeningVideo.enabled = false;
        // 컷씬이 끝났을 때 PostProcessing Volume 다시 활성화
        postProcessVolume.enabled = true; // Volume 자체를 다시 활성화
        Debug.Log("PostProcess Volume Enabled");
    }
}

    
