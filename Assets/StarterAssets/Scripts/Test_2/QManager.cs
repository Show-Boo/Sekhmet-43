using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Q[] q;//����Ʈ�迭
    private int currentQIndex = 0;//���� �������� ����Ʈ �ε���

    void Start()
    {
        StartQ(currentQIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        // ���� ����Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ� �ش� ����Ʈ�� ���� ������Ʈ�� ����
        if (q[currentQIndex].isActive)
        {
            q[currentQIndex].UpdateQuest();
        }
    }

    public void StartQ(int questIndex)
    {
        if (questIndex < q.Length)
        {
            Debug.Log($"Starting Quest: {q[questIndex].questName}");
            q[questIndex].isActive = true;
        }
    }

    public void CompleteObjective(int questIndex)
    {
        if (questIndex == currentQIndex && q[questIndex].isActive)
        {
            q[questIndex].CompleteQuest();
            currentQIndex++;

            // ���� ����Ʈ�� �ִٸ� ����
            if (currentQIndex < q.Length)
            {
                StartQ(currentQIndex);
            }
        }
        else
        {
            Debug.LogWarning($"Quest {questIndex} cannot be completed yet. Complete the current quest first.");
        }
    }

}
