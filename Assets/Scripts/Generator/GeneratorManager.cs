using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager Instance { get; private set; }

    private int fixedGeneratorCount = 0;
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
        
        Debug.Log(fixedGeneratorCount);

        if (fixedGeneratorCount >= totalGenerators && !hangarLightsOn)
        {
            hangarLightsOn = true;
            StartCoroutine(TurnOnHangarLights());

            Debug.Log("Lights On");

            audioSource.Play();
        }
    }

    private IEnumerator TurnOnHangarLights()
    {
        // 격납고에 불을 켜는 로직을 여기에 작성
        if (hangarLight != null)
        {
            hangarLight.SetActive(true); // 불을 켬
            //Debug.Log("격납고에 불이 켜졌습니다!");

            yield return new WaitForSeconds(0.5f); // 0.3초 대기
            hangarLight.SetActive(false); // 불을 끔
            //Debug.Log("격납고에 불이 꺼졌습니다.");

            yield return new WaitForSeconds(0.2f); // 0.5초 대기
            hangarLight.SetActive(true); // 불을 다시 켬
            //Debug.Log("격납고에 불이 다시 켜졌습니다.");

            yield return new WaitForSeconds(0.2f); // 0.3초 대기
            hangarLight.SetActive(false); // 불을 끔
            //Debug.Log("격납고에 불이 꺼졌습니다.");

            yield return new WaitForSeconds(0.2f); // 0.5초 대기
            hangarLight.SetActive(true); // 불을 다시 켬
            Debug.Log("격납고에 불이 다시 켜졌습니다.");
        }
        else
        {
            Debug.LogError("Hangar light object is not assigned in the Inspector.");
        }
    }
}

