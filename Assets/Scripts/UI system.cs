using UnityEngine;

public class DisplayUIOnApproach : MonoBehaviour
{
    public GameObject player;  // �÷��̾� ������Ʈ
    public GameObject textUI;  // �ؽ�Ʈ UI ������Ʈ
    public float triggerDistance = 5.0f;  // UI�� ǥ�õ� �Ÿ�

    public bool isFirst = false;

    void Update()
    {
        // �÷��̾�� ��(�� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ)�� �Ÿ� ���
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �÷��̾ ���� �Ÿ� ���� ������ UI Ȱ��ȭ
        if (distance <= triggerDistance)
        {
            textUI.SetActive(true);
            if (!isFirst)
            {
                isFirst = true;
            }
        }
        else
        {
            textUI.SetActive(false);
        }
    }
}


