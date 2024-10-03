using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject[] questUIs; // �� ����Ʈ�� �����ϴ� UI���� �迭�� ����
    private int currentQuest = 0; // ���� ���� ���� ����Ʈ ��ȣ
    private Coroutine currentCoroutine; // ���� ���� ���� �ڷ�ƾ�� ����

    void Start()
    {
        // ��� UI�� ��Ȱ��ȭ
        foreach (GameObject ui in questUIs)
        {
            ui.SetActive(false);
        }
    }

    void Update()
    {
        // QŰ�� ������ ���� ����Ʈ UI�� 3�� ���� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ���� ���� ���� �ڷ�ƾ�� ������ ����
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // ���ο� �ڷ�ƾ ����
            currentCoroutine = StartCoroutine(DisplayQuestUIForTime(3f)); // 3�� ���� UI Ȱ��ȭ
            
        }
    }

    // ����Ʈ ���� �� UI�� ������Ʈ�ϴ� �Լ�
    public void SetCurrentQuest(int questNumber)
    {
        currentQuest = questNumber;
       

    }
   
    // ���� ����Ʈ�� �´� UI�� 3�� ���� Ȱ��ȭ�ϴ� �ڷ�ƾ
    IEnumerator DisplayQuestUIForTime(float duration)
    {
        
        // ��� UI ��Ȱ��ȭ
        foreach (GameObject ui in questUIs)
        {
            ui.SetActive(false);
        }
        
        // ���� ����Ʈ UI�� Ȱ��ȭ
        if (currentQuest >= 0 && currentQuest < questUIs.Length)
        {
            
            questUIs[currentQuest].SetActive(true);
        }

        // duration��ŭ ���
        yield return new WaitForSeconds(duration);

        // �ٽ� UI ��Ȱ��ȭ
        if (currentQuest >= 0 && currentQuest < questUIs.Length)
        {
            questUIs[currentQuest].SetActive(false);
        }
    }
}
