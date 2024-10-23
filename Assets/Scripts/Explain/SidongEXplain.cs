using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SidongEXplain : MonoBehaviour
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
            messageText.text = "전력 공급 장치를 클릭하여 전력을 복구하십시오.";
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
