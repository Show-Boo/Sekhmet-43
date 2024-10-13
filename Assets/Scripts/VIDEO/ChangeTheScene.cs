using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ChangeTheScene : MonoBehaviour
{
    public GameObject loadingScreen; // 검은 배경의 로딩 화면 오브젝트
    public Image loadingSpinner; // 하얀색 선이 원형으로 빙글빙글 도는 로딩 스피너 이미지

    private void Start()
    {
        // 로딩 화면을 처음엔 비활성화
        loadingScreen.SetActive(false);
        //loadingSpinner.SetActive(false);
    }

    // 씬 전환 시작. 이게 활성화되며 씬 전환 시작
    public void StartLoadingScene(string sceneName)
    {
        // 로딩 시작 코루틴 호출
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // 로딩 화면 활성화
        loadingScreen.SetActive(true);

        // 비동기적으로 씬 로드 시작
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // 로딩 완료 후에도 씬을 즉시 전환하지 않도록 설정

        // 로딩 진행 중일 때까지 기다림
        while (!operation.isDone)
        {
            // 로딩 스피너 회전
            loadingSpinner.transform.Rotate(0f, 0f, -200f * Time.deltaTime); // 빙글빙글 돌게 설정

            // 로딩이 완료되면 씬 전환
            if (operation.progress >= 0.9f)
            {
                // 로딩이 완료되면 씬 활성화
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // 로딩 완료 후 로딩 화면 비활성화
        loadingScreen.SetActive(false);
    }
}

