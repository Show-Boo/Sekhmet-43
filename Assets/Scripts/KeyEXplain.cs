using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI messageText; // TextMeshProUGUI ������Ʈ

    void Start()
    {
        // ���� ���� �� �ؽ�Ʈ ��Ȱ��ȭ
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "ī��Ű�� EŰ�� ���� ȹ���Ͻʽÿ�.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
        }
    }
}
