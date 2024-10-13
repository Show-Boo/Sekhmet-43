using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ChangeTheScene : MonoBehaviour
{
    public GameObject loadingScreen; // ���� ����� �ε� ȭ�� ������Ʈ
    public Image loadingSpinner; // �Ͼ�� ���� �������� ���ۺ��� ���� �ε� ���ǳ� �̹���

    private void Start()
    {
        // �ε� ȭ���� ó���� ��Ȱ��ȭ
        loadingScreen.SetActive(false);
        //loadingSpinner.SetActive(false);
    }

    // �� ��ȯ ����. �̰� Ȱ��ȭ�Ǹ� �� ��ȯ ����
    public void StartLoadingScene(string sceneName)
    {
        // �ε� ���� �ڷ�ƾ ȣ��
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // �ε� ȭ�� Ȱ��ȭ
        loadingScreen.SetActive(true);

        // �񵿱������� �� �ε� ����
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // �ε� �Ϸ� �Ŀ��� ���� ��� ��ȯ���� �ʵ��� ����

        // �ε� ���� ���� ������ ��ٸ�
        while (!operation.isDone)
        {
            // �ε� ���ǳ� ȸ��
            loadingSpinner.transform.Rotate(0f, 0f, -200f * Time.deltaTime); // ���ۺ��� ���� ����

            // �ε��� �Ϸ�Ǹ� �� ��ȯ
            if (operation.progress >= 0.9f)
            {
                // �ε��� �Ϸ�Ǹ� �� Ȱ��ȭ
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // �ε� �Ϸ� �� �ε� ȭ�� ��Ȱ��ȭ
        loadingScreen.SetActive(false);
    }
}

