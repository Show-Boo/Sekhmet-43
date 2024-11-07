using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TryAgain;
    private bool first = true;

    public GameObject Youdied;
    private Button button;
    private bool buttonOn = false;

    public EnemyMove[] EnemyMove;

    void Start()
    {
        if (first)
        {
            first = false;
            gameObject.SetActive(false);
        }

        button = TryAgain.GetComponentInChildren<Button>();

    }

    void OnEnable()
    {
        // 오브젝트가 활성화될 때 호출할 함수
        TryAgain.SetActive(false);
        buttonOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("InvokeTryagain", 2f);

        if (buttonOn)
        {
            button.onClick.AddListener(HideImage);

            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키는 KeyCode.Return으로 감지. 일단 엔터키로 대체
            {
                HideImage();
            }
        }
        
    }

    public void InvokeTryagain()
    {
        TryAgain.SetActive(true);

        buttonOn = true;

    }

    public void HideImage()
    {
        Debug.Log("button");

        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = false;//다시 공격 재개
            enemyMoveScript.retry = false;//위치조정 끝
        }

        gameObject.SetActive(false);
    }
}
