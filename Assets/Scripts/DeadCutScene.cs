using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

public class DeadCutScene : MonoBehaviour
{
    // Start is called before the first frame update
    public CheckPoint checkPoint;
   
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//자체 오브젝트에서 가져오기

        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;//함수등록
    }

    // Update is called once per frame
    void StartControl(VideoPlayer vp)
    {
        checkPoint.restart = true;
        Debug.Log("checkpoint");//디버그 되는데..
    }

    void EndControl(VideoPlayer vp)
    {
        
    }
}
