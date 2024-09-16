using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager Instance { get; private set; }

    private int fixedGeneratorCount = 0; // 수리 완료된 발전기 수
    private int totalGenerators = 3; // 발전기의 총 수
    private bool hangarLightsOn = false;

    public GameObject hangarLight;

    public GameObject targetObject; // 회전시킬 대상 오브젝트
    public float rotationSpeed = 100f; // 회전 속도

    public GameObject audioPlayer; // AudioPlayer 오브젝트를 참조할 변수

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬으로 넘어가도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }

        if (audioPlayer != null)
        {
            audioSource = audioPlayer.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (hangarLightsOn)
        {
            targetObject.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }

    public void RepairGenerator()
    {
        fixedGeneratorCount++;
        Debug.Log($"수리된 발전기 수: {fixedGeneratorCount}/{totalGenerators}");

        // 모든 발전기가 수리되었는지 체크
        if (fixedGeneratorCount >= totalGenerators && !hangarLightsOn)
        {
            hangarLightsOn = true;
            StartCoroutine(TurnOnHangarLights());
            TriggerFinalEvent(); // 모든 발전기가 수리되었을 때 실행할 메서드 호출
            Debug.Log("모든 발전기가 완료되었습니다."); // 메시지 출력
            audioSource.Play();
        }
    }

    private void TriggerFinalEvent()
    {
        // 모든 발전기가 수리되었을 때 발생할 이벤트를 여기에 작성
        Debug.Log("모든 발전기가 작동합니다!"); // 예시로 로그 출력
        // 예: 게임에서 특정 행동을 트리거하는 코드 추가
        // 예를 들어, UI를 활성화하거나, 새로운 적이 등장하게 하거나 하는 등의 작업
    }

    private IEnumerator TurnOnHangarLights()
    {
        if (hangarLight != null)
        {
            hangarLight.SetActive(true); // 불을 켬
            yield return new WaitForSeconds(0.5f); // 0.5초 대기
            hangarLight.SetActive(false); // 불을 끔
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
            hangarLight.SetActive(true); // 불을 다시 켬
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
            hangarLight.SetActive(false); // 불을 끔
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
            hangarLight.SetActive(true); // 불을 다시 켬
            Debug.Log("격납고에 불이 다시 켜졌습니다.");
        }
        else
        {
            Debug.LogError("Hangar light object is not assigned in the Inspector.");
        }
    }
}
