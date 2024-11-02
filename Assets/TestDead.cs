using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TestDead : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deadCutScene;
    public VideoPlayer Dead;
    public GameObject Player;

    private void Start()
    {
        Dead= deadCutScene.GetComponent<VideoPlayer>();
        Dead.targetCamera = Player.GetComponentInChildren<Camera>();//�״� �ƾ� Ʋ���ֱ�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            deadCutScene.SetActive(true);
        }
    }


   
}
