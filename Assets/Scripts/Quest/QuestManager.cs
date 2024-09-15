using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //0913 ���� �ڵ�� ������ �� �ʿ��ҵ�
    // Start is called before the first frame update
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

    public void CompleteObjective(int questIndex)
    {
        if (questIndex == currentQuestIndex && Quest[questIndex].isActive)
        {
            Quest[questIndex].CompleteQuest();
            currentQuestIndex++;

            // ���� ����Ʈ�� �ִٸ� ����
            if (currentQuestIndex < Quest.Length)
            {
                StartQuest(currentQuestIndex);
            }
        }
        else//����Ʈ�� ���� ��쿡 �� ������ ��������..?�Ƹ�..
        {
            Debug.LogWarning($"Quest {questIndex} cannot be completed yet. Complete the current quest first.");
        }
    }
    

}
