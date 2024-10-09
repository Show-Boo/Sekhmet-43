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
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//��ü ������Ʈ���� ��������

        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;//�Լ����
    }

    // Update is called once per frame
    void StartControl(VideoPlayer vp)
    {
        checkPoint.restart = true;
        Debug.Log("checkpoint");//����� �Ǵµ�..
    }

    void EndControl(VideoPlayer vp)
    {
        
    }
}
