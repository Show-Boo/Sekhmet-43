using UnityEngine;

public class ToggleUIWithKey : MonoBehaviour
{
    public GameObject uiElement;  // 화면에 고정될 UI 요소
    public KeyCode toggleKey = KeyCode.Q;  // UI를 토글할 키 (Q 키)

    void Update()
    {
        // 설정된 키가 눌리면 UI 오브젝트의 활성화 상태를 토글
        if (Input.GetKeyDown(toggleKey))
        {
            uiElement.SetActive(!uiElement.activeSelf);
        }
    }
}


