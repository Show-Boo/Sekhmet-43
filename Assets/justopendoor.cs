using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justopendoor : MonoBehaviour
{
    public Animation hingehere;

    // Start is called before the first frame update
    void Start()
    {
        hingehere.Play(); // 게임 시작 시 문을 여는 애니메이션 실행
    }

    // Update is called once per frame
    void Update()
    {
        // Update 메서드는 필요 없으므로 비워둡니다.
    }
}

