using UnityEngine;

public class ButtonQuestManager : MonoBehaviour // ����Ʈ9 ��ư ���� ���ּ� �۵�
{
    private bool playerInRange = false; // �÷��̾ ��ư ���� ���� �ִ��� Ȯ��
    private bool questCompleted = false; // ����Ʈ �Ϸ� ����

    void Update()
    {
        // �÷��̾ ��ư ���� ���� �ְ� E Ű�� ������ ����Ʈ �Ϸ�
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !questCompleted)
        {
            CompleteQuest();
        }
    }

    // ����Ʈ �Ϸ� ó��
    private void CompleteQuest()
    {
        questCompleted = true;
        Debug.Log("����Ʈ �Ϸ�!");
        // ����Ʈ �Ϸ� �� �߰� �۾� (��: UI ǥ��, ���� ���� ��)
    }

    // �÷��̾ ��ư ���� ���� ������ ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("�÷��̾ ��ư ���� ���� ���Խ��ϴ�.");
        }
    }

    // �÷��̾ ��ư ���� ������ ������ ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("�÷��̾ ��ư ������ ������ϴ�.");
        }
    }
}
