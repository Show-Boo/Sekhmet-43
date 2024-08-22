using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager questManager;

    void Start()
    {
        // ����Ʈ �Ŵ��� ã��
        questManager = FindObjectOfType<QuestManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���� �ڽ��� ������ ��
        if (other.CompareTag("Player"))
        {
            questManager.CompleteCurrentQuest();
            gameObject.SetActive(false); // Ʈ���� �ڽ� ��Ȱ��ȭ
        }
    }
}