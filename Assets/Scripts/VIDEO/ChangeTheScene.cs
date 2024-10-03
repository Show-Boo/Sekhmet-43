using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public LoadingScreenController loadingScreenController;

    // �� ��ȯ ����

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
        // ���� ȭ�� ǥ��
        loadingScreenController.ShowBlackScreen();

        // ���� �񵿱�� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // �� �ε尡 �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // �� �ε� �Ϸ� �� ���� ȭ�� �����
        loadingScreenController.HideBlackScreen();
    }
}

