using UnityEngine;

public class Quest1_MoveToLocation : MonoBehaviour
{
    public GameObject cutscene1;  // Ȱ��ȭ�� cutscene1 ������Ʈ
    public QuestManager questManager;  // ����Ʈ �Ŵ��� ����

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
            }
        }
        else
        {
            Debug.LogError("QuestManager is not assigned in the Inspector!");
        }
    }
}
