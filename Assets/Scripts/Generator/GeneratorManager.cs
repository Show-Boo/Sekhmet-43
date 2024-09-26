using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager Instance { get; private set; }

    private int fixedGeneratorCount = 0;
    private int totalGenerators = 3; // 발전기의 총 수
    public bool hangarLightsOn = false;

    public GameObject hangarLight;
    public GameObject targetObject; // 회전시킬 대상 오브젝트
    public float rotationSpeed = 100f; // 회전 속도
    public GameObject audioPlayer; // AudioPlayer 오브젝트를 참조할 변수

    private AudioSource audioSource;

    private HashSet<int> completedGenerators = new HashSet<int>(); // 완료된 발전기 ID를 추적

    //public Q_7 q_7;
    public bool engineIsAllFixed = false;//아니 왜 true해놓으니가 또 안되냐


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


    public void RepairGenerator(int generatorId)
    {
        // 함수 호출 여부 확인
        //Debug.Log("RepairGenerator 함수가 호출되었습니다. ID: " + generatorId);

        // 이미 완료된 발전기라면 무시
        if (completedGenerators.Contains(generatorId))
        {
            Debug.Log("발전기 " + generatorId + "은(는) 이미 완료되었습니다.");
            return;
        }

        // 발전기 수리 완료 처리
        completedGenerators.Add(generatorId);
        fixedGeneratorCount++;

        // HashSet 업데이트 확인
        Debug.Log("발전기 " + generatorId + " 완료 (" + fixedGeneratorCount + "/" + totalGenerators + ")");
        //Debug.Log("완료된 발전기 ID 목록: " + string.Join(", ", completedGenerators));

        // 모든 발전기가 완료되었는지 확인
        if (fixedGeneratorCount >= totalGenerators)
        {
            Debug.Log("모든 발전기가 완료되었습니다.");
            OnAllGeneratorsFixed();
        }
    }




    private void OnAllGeneratorsFixed()
    {
        // 여기서 모든 발전기 완료 시 수행할 동작을 정의
        Debug.Log("모든 발전기 수리가 완료되었습니다. 작업이 끝났습니다.");
        //q_7.q_7_done = true;//여기서 퀘스트 성공
        engineIsAllFixed = true;//엔진 다 고쳐짐!
    }


    private void Update()
    {
        if (hangarLightsOn)
        {
            targetObject.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }



    public void RepairGenerator()//Q_6trigger에서 쓰입
    {
        //fixedGeneratorCount++;
        //Debug.Log($"수리된 발전기 수: {fixedGeneratorCount}/{totalGenerators}");
        StartCoroutine(TurnOnHangarLights());
        audioSource.Play();
        hangarLightsOn = true;//엔진 돌리기
        // 모든 발전기가 수리되었는지 체크
        /*
        if (fixedGeneratorCount >= totalGenerators && !hangarLightsOn)
        {
            hangarLightsOn = true;
            StartCoroutine(TurnOnHangarLights());
            //TriggerFinalEvent(); // 모든 발전기가 수리되었을 때 실행할 메서드 호출
            //q_7.q_7_done = true;
            Debug.Log("모든 발전기가 완료되었습니다."); // 메시지 출력
            audioSource.Play();
        }
        */
    }
    /*
    private void TriggerFinalEvent()
    {
        // 모든 발전기가 수리되었을 때 발생할 이벤트를 여기에 작성
        //Debug.Log("모든 발전기가 작동합니다!"); // 예시로 로그 출력
        // 예: 게임에서 특정 행동을 트리거하는 코드 추가
        // 예를 들어, UI를 활성화하거나, 새로운 적이 등장하게 하거나 하는 등의 작업
        
    }
    */

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
