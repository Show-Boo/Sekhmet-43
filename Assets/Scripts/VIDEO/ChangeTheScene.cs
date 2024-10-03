using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public LoadingScreenController loadingScreenController;

    // 씬 전환 시작

    public void Start()
    {
        LoadSceneWithBlackScreen("myproject");
    }
    public void LoadSceneWithBlackScreen(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        // 검은 화면 표시
        loadingScreenController.ShowBlackScreen();

        // 씬을 비동기로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // 씬 로드가 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // 씬 로드 완료 후 검은 화면 숨기기
        loadingScreenController.HideBlackScreen();
    }
}

