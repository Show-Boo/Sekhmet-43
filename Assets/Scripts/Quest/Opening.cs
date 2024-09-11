using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Opening : MonoBehaviour
{

    public VideoPlayer OpeningVideo; // VideoPlayer 컴포넌트를 연결합니다.
    public PlayerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        OpeningVideo.started += DisablePlayerControl;
        OpeningVideo.loopPointReached += EnablePlayerControl;
        OpeningVideo.Play();
        Debug.Log("opening video play");
    }

    // Update is called once per frame
    void DisablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = false;//컷씬 시작하면 player 멈춰주기
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;//컷씬 끝나면 player 이동 가능하게 해주기
        OpeningVideo.enabled = false;
    }
}

    
