using System;
using UnityEngine;

[Serializable]
public class Quest
{
    public string questName;
    public string description;
    public GameObject[] activationTriggers;   // ����Ʈ ���� �� Ȱ��ȭ�� Ʈ���� �ڽ���
    public GameObject[] deactivationTriggers; // ����Ʈ �Ϸ� �� ��Ȱ��ȭ�� Ʈ���� �ڽ���
    public Action onStart;                    // ����Ʈ ���� �� ȣ��� �̺�Ʈ
    public Action onComplete;                 // ����Ʈ �Ϸ� �� ȣ��� �̺�Ʈ
    public string videoPath;                  // ����Ʈ�� ����� ������ ���� ���
    public bool isCompleted;
}
