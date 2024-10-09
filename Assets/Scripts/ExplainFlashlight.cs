using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가
using System.Collections; // 코루틴을 사용하기 위한 네임스페이스

public class TriggerBox : MonoBehaviour
{
    public TextMeshProUGUI messageText; // TextMeshProUGUI를 사용
    public float fadeDuration = 1f; // 서서히 사라지는 데 걸리는 시간
    public float displayTime = 3f; // 텍스트가 보이는 시간
    private Coroutine fadeCoroutine;

    public Vector3 allowedDirection = Vector3.forward; // 허용된 진입 방향
    public float directionThreshold = 2f; // 진입 방향의 내적 계산 임계값

    void Start()
    {
        messageText.alpha = 0; // 텍스트의 투명도를 0으로 설정해 처음에 보이지 않게 함
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌할 경우
        {
            // 플레이어가 트리거 박스로 진입한 방향 계산
            Vector3 enteringDirection = (transform.position - other.transform.position).normalized;

            // 진입 방향과 허용된 방향의 내적 계산
            float dotProduct = Vector3.Dot(enteringDirection, allowedDirection.normalized);

            // 진입 방향이 허용된 방향과 일치하는지 확인
            if (dotProduct >= directionThreshold)
            {
                // 이전에 실행 중이던 코루틴이 있으면 중지
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }

                // 텍스트를 완전히 표시한 후 페이드아웃 시작
                messageText.alpha = 1; // 텍스트를 완전히 보이게 설정
                fadeCoroutine = StartCoroutine(FadeOutText()); // 페이드아웃 코루틴 시작
            }
        }
    }

    private IEnumerator FadeOutText()
    {
        // 일정 시간 동안 텍스트 표시
        yield return new WaitForSeconds(displayTime);

        // 서서히 투명도를 낮추는 루프
        float elapsedTime = 0f;
        Color originalColor = messageText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        messageText.alpha = 0; // 최종적으로 완전히 숨기기
    }
}
