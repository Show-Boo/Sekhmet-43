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

    //�ƾ� ������ ���� ȭ�� Ȱ��ȭ+�״� �ƾ� ��Ȱ��ȭ -> text ������ ���� -> ����Űor��ư������ -> ����ȭ�� ����� + ȭ�� ��Ȱ��ȭ.

    public GameObject blackOut;
    void Start()
    {
        //blackOut.SetActive(false);�� ��ũ��Ʈ�� �װ� ���� Ȱ��ȭ �ǹǷ� �� �ڵ�� ���ǹ���
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//��ü ������Ʈ���� ��������

        videoPlayer.started += StartControl;
        videoPlayer.loopPointReached += EndControl;//�Լ����

    }

    // Update is called once per frame
    void StartControl(VideoPlayer vp)
    {
        
    }

    void EndControl(VideoPlayer vp)
    {
        checkPoint.restart = true;//�ƾ� ������ ���ư���
        Debug.Log("checkpoint");//����� �Ǵµ�..

        blackOut.SetActive(true);//����ȭ��

        gameObject.SetActive(false);//��ü ��Ȱ��ȭ


    }

    
}
