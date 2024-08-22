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

            // 필요한 트리거 박스 활성화
            foreach (var trigger in currentQuest.activationTriggers)
            {
                trigger.SetActive(true);
            }

            // 동영상 재생
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

            // 완료 시 불필요한 트리거 비활성화
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
        questManager.StartQuest(0); // 첫 퀘스트 시작
    }

    void SetupQuests()
    {
        // 퀘스트 1 설정
        Quest quest1 = new Quest
        {
            questName = "퀘스트 1",
            description = "지하 2층 연구실로 향하세요",
            activationTriggers = new GameObject[] { GameObject.Find("Quest1Trigger") },
            deactivationTriggers = new GameObject[] { },
            videoPath = "Assets/Video/Cutscene1.mp4",
            onComplete = () => { Debug.Log("퀘스트 1 완료"); }
        };

        // 퀘스트 2 설정
        Quest quest2 = new Quest
        {
            questName = "퀘스트 2",
            description = "지하 1층 방을 수색하여 연구 물품을 찾으세요",
            activationTriggers = new GameObject[] { GameObject.Find("Quest2Trigger") },
            deactivationTriggers = new GameObject[] { GameObject.Find("Quest1Trigger") },
            videoPath = "Assets/Video/Cutscene2.mp4",
            onComplete = () => { Debug.Log("퀘스트 2 완료"); }
        };

        questManager.quests.Add(quest1);
        questManager.quests.Add(quest2);
    }
}
