using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager Instance { get; private set; }

    private int fixedGeneratorCount = 0;
    private int totalGenerators = 3; // �������� �� ��
    public bool hangarLightsOn = false;

    public GameObject hangarLight;
    public GameObject targetObject; // ȸ����ų ��� ������Ʈ
    public float rotationSpeed = 100f; // ȸ�� �ӵ�
    public GameObject audioPlayer; // AudioPlayer ������Ʈ�� ������ ����

    private AudioSource audioSource;

    private HashSet<int> completedGenerators = new HashSet<int>(); // �Ϸ�� ������ ID�� ����

    //public Q_7 q_7;
    public bool engineIsAllFixed = false;//�ƴ� �� true�س����ϰ� �� �ȵǳ�


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �ٸ� ������ �Ѿ�� �ı����� ����
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
        // �Լ� ȣ�� ���� Ȯ��
        //Debug.Log("RepairGenerator �Լ��� ȣ��Ǿ����ϴ�. ID: " + generatorId);

        // �̹� �Ϸ�� �������� ����
        if (completedGenerators.Contains(generatorId))
        {
            Debug.Log("������ " + generatorId + "��(��) �̹� �Ϸ�Ǿ����ϴ�.");
            return;
        }

        // ������ ���� �Ϸ� ó��
        completedGenerators.Add(generatorId);
        fixedGeneratorCount++;

        // HashSet ������Ʈ Ȯ��
        Debug.Log("������ " + generatorId + " �Ϸ� (" + fixedGeneratorCount + "/" + totalGenerators + ")");
        //Debug.Log("�Ϸ�� ������ ID ���: " + string.Join(", ", completedGenerators));

        // ��� �����Ⱑ �Ϸ�Ǿ����� Ȯ��
        if (fixedGeneratorCount >= totalGenerators)
        {
            Debug.Log("��� �����Ⱑ �Ϸ�Ǿ����ϴ�.");
            OnAllGeneratorsFixed();
        }
    }




    private void OnAllGeneratorsFixed()
    {
        // ���⼭ ��� ������ �Ϸ� �� ������ ������ ����
        Debug.Log("��� ������ ������ �Ϸ�Ǿ����ϴ�. �۾��� �������ϴ�.");
        //q_7.q_7_done = true;//���⼭ ����Ʈ ����
        engineIsAllFixed = true;//���� �� ������!
    }


    private void Update()
    {
        if (hangarLightsOn)
        {
            targetObject.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }



    public void RepairGenerator()//Q_6trigger���� ����
    {
        //fixedGeneratorCount++;
        //Debug.Log($"������ ������ ��: {fixedGeneratorCount}/{totalGenerators}");
        StartCoroutine(TurnOnHangarLights());
        audioSource.Play();
        hangarLightsOn = true;//���� ������
        // ��� �����Ⱑ �����Ǿ����� üũ
        /*
        if (fixedGeneratorCount >= totalGenerators && !hangarLightsOn)
        {
            hangarLightsOn = true;
            StartCoroutine(TurnOnHangarLights());
            //TriggerFinalEvent(); // ��� �����Ⱑ �����Ǿ��� �� ������ �޼��� ȣ��
            //q_7.q_7_done = true;
            Debug.Log("��� �����Ⱑ �Ϸ�Ǿ����ϴ�."); // �޽��� ���
            audioSource.Play();
        }
        */
    }
    /*
    private void TriggerFinalEvent()
    {
        // ��� �����Ⱑ �����Ǿ��� �� �߻��� �̺�Ʈ�� ���⿡ �ۼ�
        //Debug.Log("��� �����Ⱑ �۵��մϴ�!"); // ���÷� �α� ���
        // ��: ���ӿ��� Ư�� �ൿ�� Ʈ�����ϴ� �ڵ� �߰�
        // ���� ���, UI�� Ȱ��ȭ�ϰų�, ���ο� ���� �����ϰ� �ϰų� �ϴ� ���� �۾�
        
    }
    */

    private IEnumerator TurnOnHangarLights()
    {
        if (hangarLight != null)
        {
            hangarLight.SetActive(true); // ���� ��
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
            hangarLight.SetActive(false); // ���� ��
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
            hangarLight.SetActive(true); // ���� �ٽ� ��
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
            hangarLight.SetActive(false); // ���� ��
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
            hangarLight.SetActive(true); // ���� �ٽ� ��
            Debug.Log("�ݳ��� ���� �ٽ� �������ϴ�.");
        }
        else
        {
            Debug.LogError("Hangar light object is not assigned in the Inspector.");
        }
    }


}
