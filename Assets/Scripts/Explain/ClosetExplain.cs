using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class closetexplain : MonoBehaviour
{
    public TextMeshProUGUI messageText; // TextMeshProUGUI 컴포넌트

    void Start()
    {
        // 게임 시작 시 텍스트 비활성화
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "E키를 눌러 옷장에 숨을 수 있습니다.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false); // 텍스트 비활성화
        }
    }
}
