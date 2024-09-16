using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //0913 ���� �ڵ�� ������ �� �ʿ��ҵ�
    
    public Q_ParentClass[] Quest;//����Ʈ�迭
    private int currentQuestIndex = 0;//���� �������� ����Ʈ �ε���

    
    void Start()
    {
        StartQuest(currentQuestIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        // ���� ����Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ� �ش� ����Ʈ�� ���� ������Ʈ�� ����
        if (Quest[currentQuestIndex].isActive)
        {
            Quest[currentQuestIndex].UpdateQuest();
        }
    }

    public void StartQuest(int questIndex)
    {
        if (questIndex < Quest.Length)
        {
            Debug.Log($"Starting Quest: {Quest[questIndex].questName}");
            Quest[questIndex].isActive = true;
        }
    }

    public void CompleteObjective()
    {
        if (Quest[currentQuestIndex].isActive)
        {
            Quest[currentQuestIndex].CompleteQuest();
            currentQuestIndex++;

            // ���� ����Ʈ�� �ִٸ� ����
            if (currentQuestIndex < Quest.Length)
            {
                StartQuest(currentQuestIndex);
            }
        }
        else//����Ʈ�� ���� ��쿡 �� ������ ��������..?�Ƹ�..�ƴϾ߱׷���������
        {
            Debug.LogWarning($"Quest {currentQuestIndex} cannot be completed yet. Complete the current quest first.");
        }
    }
    

}
