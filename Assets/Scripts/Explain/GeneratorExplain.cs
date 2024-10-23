using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneratorExplain : MonoBehaviour
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
            messageText.text = "발전기를 수리하려면 마우스 왼쪽 버튼으로 길게 누르시오.";
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
