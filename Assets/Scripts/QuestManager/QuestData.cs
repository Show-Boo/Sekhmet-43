using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int questID;  // ����Ʈ ID
    public string questName;  // ����Ʈ �̸�
    public string description;  // ����Ʈ ����
    public bool isCompleted;  // ����Ʈ �Ϸ� ����

    // �䱸 ���׵� (��: Ư�� ������ ����, Ư�� ��� ���� ��)
    public List<string> objectives = new List<string>();

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log("Quest " + questName + " completed!");
        // ���� ���� �Ǵ� ���� ����Ʈ Ȱ��ȭ �ڵ� �߰�
    }
}
