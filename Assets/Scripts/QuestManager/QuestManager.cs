using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public static QuestManager instance;

    public GameObject cutscene1;  // cutscene1 오브젝트 참조

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

            // 모든 요구사항이 완료되었는지 확인
            if (quest.objectives.Count == 0)
            {
                quest.CompleteQuest();

                // 퀘스트 1이 완료되면 cutscene1 오브젝트를 활성화
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
            // 퀘스트 시작에 대한 로직 추가 (예: 퀘스트 UI 업데이트)
        }
    }
}
