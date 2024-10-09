using UnityEngine;
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽� �߰�
using System.Collections; // �ڷ�ƾ�� ����ϱ� ���� ���ӽ����̽�

public class TriggerBox : MonoBehaviour
{
    public TextMeshProUGUI messageText; // TextMeshProUGUI�� ���
    public float fadeDuration = 1f; // ������ ������� �� �ɸ��� �ð�
    public float displayTime = 3f; // �ؽ�Ʈ�� ���̴� �ð�
    private Coroutine fadeCoroutine;

    public Vector3 allowedDirection = Vector3.forward; // ���� ���� ����
    public float directionThreshold = 2f; // ���� ������ ���� ��� �Ӱ谪

    void Start()
    {
        messageText.alpha = 0; // �ؽ�Ʈ�� ������ 0���� ������ ó���� ������ �ʰ� ��
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹�� ���
        {
            // �÷��̾ Ʈ���� �ڽ��� ������ ���� ���
            Vector3 enteringDirection = (transform.position - other.transform.position).normalized;

            // ���� ����� ���� ������ ���� ���
            float dotProduct = Vector3.Dot(enteringDirection, allowedDirection.normalized);

            // ���� ������ ���� ����� ��ġ�ϴ��� Ȯ��
            if (dotProduct >= directionThreshold)
            {
                // ������ ���� ���̴� �ڷ�ƾ�� ������ ����
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }

                // �ؽ�Ʈ�� ������ ǥ���� �� ���̵�ƿ� ����
                messageText.alpha = 1; // �ؽ�Ʈ�� ������ ���̰� ����
                fadeCoroutine = StartCoroutine(FadeOutText()); // ���̵�ƿ� �ڷ�ƾ ����
            }
        }
    }

    private IEnumerator FadeOutText()
    {
        // ���� �ð� ���� �ؽ�Ʈ ǥ��
        yield return new WaitForSeconds(displayTime);

        // ������ ������ ���ߴ� ����
        float elapsedTime = 0f;
        Color originalColor = messageText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        messageText.alpha = 0; // ���������� ������ �����
    }
}
