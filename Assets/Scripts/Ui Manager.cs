using UnityEngine;

public class ToggleUIWithKey : MonoBehaviour
{
    public GameObject uiElement;  // ȭ�鿡 ������ UI ���
    public KeyCode toggleKey = KeyCode.Q;  // UI�� ����� Ű (Q Ű)

    void Update()
    {
        // ������ Ű�� ������ UI ������Ʈ�� Ȱ��ȭ ���¸� ���
        if (Input.GetKeyDown(toggleKey))
        {
            uiElement.SetActive(!uiElement.activeSelf);
        }
    }
}


