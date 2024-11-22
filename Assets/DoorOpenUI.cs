using UnityEngine;

public class DoorOpenUI : MonoBehaviour
{
    public GameObject player;  // �÷��̾� ������Ʈ
    public GameObject textUI1;  // ó�� ���� �� ǥ���� �ؽ�Ʈ UI ������Ʈ
    public GameObject textUI2;  // ���� ���� �� ǥ���� �ؽ�Ʈ UI ������Ʈ
    public float triggerDistance = 5.0f;  // UI�� ǥ�õ� �Ÿ�
    public OpenDoor openDoor; // �� ������Ʈ�� ���¸� Ȯ��
    public ActionController actionController; // ī��Ű ���¸� Ȯ���ϴ� ActionController ����

    public bool isFirst = false; // �÷��̾ ó�� �����ߴ��� Ȯ��

    private void Start()
    {
        // ó������ �� UI ��� ��Ȱ��ȭ
        if (textUI1 != null) textUI1.SetActive(false);
        if (textUI2 != null) textUI2.SetActive(false);

        // ActionController�� ������� ���� ��� ��� �޽���
        if (actionController == null)
        {
            Debug.LogError("ActionController�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        // ���� ���� ������ ��� UI�� ǥ������ ����
        if (openDoor != null && openDoor.isOpen)
        {
            if (textUI1 != null) textUI1.SetActive(false);
            if (textUI2 != null) textUI2.SetActive(false);
            return;
        }

        // �÷��̾���� �Ÿ� ���
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �÷��̾ ���� �Ÿ� ���� ������ UI ǥ��
        if (distance <= triggerDistance)
        {
            if (!isFirst) // ó�� ���� ��
            {
                if (textUI1 != null) textUI1.SetActive(true); // "ī��Ű�� �ʿ��մϴ�" ǥ��
                if (textUI2 != null) textUI2.SetActive(false);

                Debug.Log("�÷��̾ ó�� �����߽��ϴ�.");
                isFirst = true; // ó�� ���� ���·� ����
            }
            else // ���� ���� ��
            {
                if (actionController != null && actionController.CanOpenDoor())
                {
                    if (textUI1 != null) textUI1.SetActive(false);
                    if (textUI2 != null) textUI2.SetActive(true); // "E Ű�� ���� ���� ���ʽÿ�" ǥ��
                }
                else
                {
                    if (textUI1 != null) textUI1.SetActive(true); // ī��Ű�� ������ ó�� �޽��� ��ǥ��
                    if (textUI2 != null) textUI2.SetActive(false);
                }
            }
        }
        else
        {
            // �÷��̾ ���� �Ÿ� �ۿ� ������ UI ��Ȱ��ȭ
            if (textUI1 != null) textUI1.SetActive(false);
            if (textUI2 != null) textUI2.SetActive(false);
        }
    }
}
