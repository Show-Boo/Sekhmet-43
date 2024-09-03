using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Q : MonoBehaviour
{
    //parent class
    // Start is called before the first frame update

    public string questName;  // 퀘스트 이름
    public virtual bool cutscene { get; set; } = true;  // 퀘스트 완료 시 재생될 컷씬 (필요한 경우)
    public bool isActive = false;  // 퀘스트 활성화 여부
    public bool isCompleted = false;  // 퀘스트 완료 여부

    public VideoPlayer videoPlayer;
    //public PlayerController playerController;
    public CutSceneController cutsceneController;
    public int cutsceneIndex=0;

    

    public virtual void UpdateQuest()
    {
        // 이 함수는 특정 퀘스트가 활성화된 동안 매 프레임마다 호출됩니다.
        // 필요한 경우 각 퀘스트에서 이 메서드를 오버라이드하여 사용할 수 있습니다.
    }
    

    public void CompleteQuest()//컷씬은 하나의 퀘스트가 끝나는 경우에 틀어짐
    {
        if (cutscene ==true)//컷씬이 있는 경우만
        {
            Debug.Log($"{questName} cutscene has been activated.");
            cutsceneController.PlayCutscene(cutsceneIndex);
            cutsceneIndex++;
        }

        isCompleted = true;
        isActive = false;
        Debug.Log($"{questName} completed!");
    }

}
