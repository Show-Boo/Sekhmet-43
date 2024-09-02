using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Q : MonoBehaviour
{
    //parent class
    // Start is called before the first frame update

    public string questName;  // ����Ʈ �̸�
    public GameObject cutscene;  // ����Ʈ �Ϸ� �� ����� �ƾ� (�ʿ��� ���)
    public bool isActive = false;  // ����Ʈ Ȱ��ȭ ����
    public bool isCompleted = false;  // ����Ʈ �Ϸ� ����

    public virtual void UpdateQuest()
    {
        // �� �Լ��� Ư�� ����Ʈ�� Ȱ��ȭ�� ���� �� �����Ӹ��� ȣ��˴ϴ�.
        // �ʿ��� ��� �� ����Ʈ���� �� �޼��带 �������̵��Ͽ� ����� �� �ֽ��ϴ�.
    }
    

    public void CompleteQuest()
    {
        if (cutscene != null)
        {
            cutscene.SetActive(true);
            Debug.Log($"{questName} cutscene has been activated.");
        }
        isCompleted = true;
        isActive = false;
        Debug.Log($"{questName} completed!");
    }

}
