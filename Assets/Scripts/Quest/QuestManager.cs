using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public Q_ParentClass[] Quest; // ����Ʈ �迭
    private int currentQuestIndex = 0; // ���� ���� ���� ����Ʈ �ε���
    public QuestUI questUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartQuest(currentQuestIndex);
    }

    private void Update()
    {
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
            questUI.SetCurrentQuest(questIndex);
        }
    }

    public void CompleteObjective()
    {
        if (Quest[currentQuestIndex].isActive)
        {
            Quest[currentQuestIndex].CompleteQuest();
            currentQuestIndex++;

            if (currentQuestIndex < Quest.Length)
            {
                StartQuest(currentQuestIndex);
            }
        }
        else
        {
            Debug.LogWarning($"Quest {currentQuestIndex} cannot be completed yet. Complete the current quest first.");
        }
    }
}
