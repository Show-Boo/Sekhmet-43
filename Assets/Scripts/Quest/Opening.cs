using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Opening : MonoBehaviour
{

    public VideoPlayer OpeningVideo; // VideoPlayer ������Ʈ�� �����մϴ�.
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
        playerController.enabled = false;//�ƾ� �����ϸ� player �����ֱ�
    }

    void EnablePlayerControl(VideoPlayer vp)
    {
        playerController.enabled = true;//�ƾ� ������ player �̵� �����ϰ� ���ֱ�
        OpeningVideo.enabled = false;
    }
}

    
