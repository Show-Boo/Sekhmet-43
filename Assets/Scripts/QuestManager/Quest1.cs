using UnityEngine;

public class Quest1_MoveToLocation : MonoBehaviour
{
    public GameObject cutscene1;  // Ȱ��ȭ�� cutscene1 ������Ʈ
    public GameObject cutscene2;  // cutscene1�� Ʈ���� �ڽ��� �����Ǹ� Ȱ��ȭ�� cutscene2 ������Ʈ
    public QuestManager questManager;  // ����Ʈ �Ŵ��� ����
    public Collider quest1Trigger;  // cutscene1 ������ Quest1Trigger Ʈ���� ����

    private void Start()
    {
        // quest1Trigger�� �����Ǿ����� Ȯ��
        if (quest1Trigger == null)
        {
            Debug.LogError("Quest1Trigger is not assigned! Please assign the trigger collider from cutscene1.");
        }
        else
        {
            // Quest1Trigger�� Ʈ���� �̺�Ʈ�� ����
            quest1Trigger.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ��
        if (other.CompareTag("Player"))
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        Debug.Log("Quest 1 completed!");

        if (questManager != null)
        {
            questManager.CompleteObjective(1, "Move to Location");

            // cutscene1 ������Ʈ Ȱ��ȭ
            if (cutscene1 != null)
            {
                cutscene1.SetActive(true);
                Debug.Log("cutscene1 has been activated.");
            }
            else
            {
                Debug.LogError("cutscene1 is not assigned in the Inspector!");
                return;
            }

            // Quest1Trigger���� Ʈ���� �̺�Ʈ ������ ���� �߰� �ݹ� ���
            quest1Trigger.gameObject.AddComponent<Quest1TriggerHandler>().Initialize(this);
        }
        else
        {
            Debug.LogError("QuestManager is not assigned in the Inspector!");
        }
    }

    // Quest1Trigger�� �÷��̾�� �浹���� �� ȣ��� �޼���
    public void ActivateCutscene2()
    {
        if (cutscene2 != null)
        {
            cutscene2.SetActive(true);
            Debug.Log("cutscene2 has been activated.");
        }
        else
        {
            Debug.LogError("cutscene2 is not assigned in the Inspector!");
        }
    }
}

public class Quest1TriggerHandler : MonoBehaviour
{
    private Quest1_MoveToLocation quest1_MoveToLocation;

    public void Initialize(Quest1_MoveToLocation quest)
    {
        quest1_MoveToLocation = quest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quest1_MoveToLocation.ActivateCutscene2();
        }
    }
}
