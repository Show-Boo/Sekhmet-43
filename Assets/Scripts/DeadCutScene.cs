using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.UI;

public class DeadCutScene : MonoBehaviour
{
    // Start is called before the first frame update
    public CheckPoint checkPoint;

    //컷씬 끝나면 검은 화면 활성화+죽는 컷씬 비활성화 -> text 깜빡임 구현 -> 엔터키or버튼누르기 -> 검은화면 사라짐 + 화면 비활성화.

    public GameObject blackOut;
    void Start()
    {
        //blackOut.SetActive(false);이 스크립트는 죽고 나서 활성화 되므로 이 코드는 무의미함
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//자체 오브젝트에서 가져오기

        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;//함수등록

    }

    // Update is called once per frame
    void StartControl(VideoPlayer vp)
    {
        
    }

    void EndControl(VideoPlayer vp)
    {
        checkPoint.restart = true;//컷씬 끝나고 돌아가기
        Debug.Log("checkpoint");//디버그 되는데..

        blackOut.SetActive(true);//검은화면

        gameObject.SetActive(false);//자체 비활성화


    }

    
}
