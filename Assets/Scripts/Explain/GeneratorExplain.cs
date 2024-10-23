using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneratorExplain : MonoBehaviour
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
            messageText.text = "�����⸦ �����Ϸ��� ���콺 ���� ��ư���� ��� �����ÿ�.";
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
