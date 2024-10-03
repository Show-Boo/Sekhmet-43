using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject[] questUIs; // 각 퀘스트에 대응하는 UI들을 배열로 관리
    private int currentQuest = 0; // 현재 실행 중인 퀘스트 번호
    private Coroutine currentCoroutine; // 현재 실행 중인 코루틴을 관리

    void Start()
    {
        // 모든 UI를 비활성화
        foreach (GameObject ui in questUIs)
        {
            ui.SetActive(false);
        }
    }

    void Update()
    {
        // Q키를 누르면 현재 퀘스트 UI를 3초 동안 활성화
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 현재 실행 중인 코루틴이 있으면 중지
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // 새로운 코루틴 시작
            currentCoroutine = StartCoroutine(DisplayQuestUIForTime(3f)); // 3초 동안 UI 활성화
            
        }
    }

    // 퀘스트 진행 중 UI를 업데이트하는 함수
    public void SetCurrentQuest(int questNumber)
    {
        currentQuest = questNumber;
       

    }
   
    // 현재 퀘스트에 맞는 UI를 3초 동안 활성화하는 코루틴
    IEnumerator DisplayQuestUIForTime(float duration)
    {
        
        // 모든 UI 비활성화
        foreach (GameObject ui in questUIs)
        {
            ui.SetActive(false);
        }
        
        // 현재 퀘스트 UI만 활성화
        if (currentQuest >= 0 && currentQuest < questUIs.Length)
        {
            
            questUIs[currentQuest].SetActive(true);
        }

        // duration만큼 대기
        yield return new WaitForSeconds(duration);

        // 다시 UI 비활성화
        if (currentQuest >= 0 && currentQuest < questUIs.Length)
        {
            questUIs[currentQuest].SetActive(false);
        }
    }
}
