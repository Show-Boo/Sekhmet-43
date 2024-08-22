using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public static QuestManager instance;

    public GameObject cutscene1;  // cutscene1 ������Ʈ ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public void CompleteObjective(int questID, string objective)
    {
        Quest quest = quests.Find(q => q.questID == questID);
        if (quest != null && quest.objectives.Contains(objective))
        {
            quest.objectives.Remove(objective);
            Debug.Log("Objective " + objective + " completed for Quest " + quest.questName);

            // ��� �䱸������ �Ϸ�Ǿ����� Ȯ��
            if (quest.objectives.Count == 0)
            {
                quest.CompleteQuest();

                // ����Ʈ 1�� �Ϸ�Ǹ� cutscene1 ������Ʈ�� Ȱ��ȭ
                if (questID == 1 && cutscene1 != null)
                {
                    cutscene1.SetActive(true);
                    Debug.Log("cutscene1 has been activated.");
                }
            }
        }
    }

    public void StartQuest(int questID)
    {
        Quest quest = quests.Find(q => q.questID == questID);
        if (quest != null && !quest.isCompleted)
        {
            Debug.Log("Starting Quest " + quest.questName);
            // ����Ʈ ���ۿ� ���� ���� �߰� (��: ����Ʈ UI ������Ʈ)
        }
    }
}
