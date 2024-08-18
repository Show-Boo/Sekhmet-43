using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    public Text questUIText;
    public VideoPlayer videoPlayer;
    private int currentQuestIndex = 0;

    void Start()
    {
        StartQuest(currentQuestIndex);
    }

    public void StartQuest(int questIndex)
    {
        if (questIndex < quests.Count)
        {
            Quest currentQuest = quests[questIndex];
            questUIText.text = currentQuest.description;

            // �ʿ��� Ʈ���� �ڽ� Ȱ��ȭ
            foreach (var trigger in currentQuest.activationTriggers)
            {
                trigger.SetActive(true);
            }

            // ������ ���
            videoPlayer.url = currentQuest.videoPath;
            videoPlayer.Play();

            currentQuest.onStart?.Invoke();
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            Quest currentQuest = quests[currentQuestIndex];
            currentQuest.isCompleted = true;

            // �Ϸ� �� ���ʿ��� Ʈ���� ��Ȱ��ȭ
            foreach (var trigger in currentQuest.deactivationTriggers)
            {
                trigger.SetActive(false);
            }

            currentQuest.onComplete?.Invoke();
            currentQuestIndex++;
            StartQuest(currentQuestIndex);
        }
    }
}

public class GameSetup : MonoBehaviour
{
    public QuestManager questManager;

    void Start()
    {
        SetupQuests();
        questManager.StartQuest(0); // ù ����Ʈ ����
    }

    void SetupQuests()
    {
        // ����Ʈ 1 ����
        Quest quest1 = new Quest
        {
            questName = "����Ʈ 1",
            description = "���� 2�� �����Ƿ� ���ϼ���",
            activationTriggers = new GameObject[] { GameObject.Find("Quest1Trigger") },
            deactivationTriggers = new GameObject[] { },
            videoPath = "Assets/Video/Cutscene1.mp4",
            onComplete = () => { Debug.Log("����Ʈ 1 �Ϸ�"); }
        };

        // ����Ʈ 2 ����
        Quest quest2 = new Quest
        {
            questName = "����Ʈ 2",
            description = "���� 1�� ���� �����Ͽ� ���� ��ǰ�� ã������",
            activationTriggers = new GameObject[] { GameObject.Find("Quest2Trigger") },
            deactivationTriggers = new GameObject[] { GameObject.Find("Quest1Trigger") },
            videoPath = "Assets/Video/Cutscene2.mp4",
            onComplete = () => { Debug.Log("����Ʈ 2 �Ϸ�"); }
        };

        questManager.quests.Add(quest1);
        questManager.quests.Add(quest2);
    }
}
