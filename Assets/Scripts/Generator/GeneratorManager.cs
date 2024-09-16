using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager Instance { get; private set; }

    private int fixedGeneratorCount = 0; // ���� �Ϸ�� ������ ��
    private int totalGenerators = 3; // �������� �� ��
    private bool hangarLightsOn = false;

    public GameObject hangarLight;

    public GameObject targetObject; // ȸ����ų ��� ������Ʈ
    public float rotationSpeed = 100f; // ȸ�� �ӵ�

    public GameObject audioPlayer; // AudioPlayer ������Ʈ�� ������ ����

    private AudioSource audioSource;

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
        Debug.Log($"������ ������ ��: {fixedGeneratorCount}/{totalGenerators}");

        // ��� �����Ⱑ �����Ǿ����� üũ
        if (fixedGeneratorCount >= totalGenerators && !hangarLightsOn)
        {
            hangarLightsOn = true;
            StartCoroutine(TurnOnHangarLights());
            TriggerFinalEvent(); // ��� �����Ⱑ �����Ǿ��� �� ������ �޼��� ȣ��
            Debug.Log("��� �����Ⱑ �Ϸ�Ǿ����ϴ�."); // �޽��� ���
            audioSource.Play();
        }
    }

    private void TriggerFinalEvent()
    {
        // ��� �����Ⱑ �����Ǿ��� �� �߻��� �̺�Ʈ�� ���⿡ �ۼ�
        Debug.Log("��� �����Ⱑ �۵��մϴ�!"); // ���÷� �α� ���
        // ��: ���ӿ��� Ư�� �ൿ�� Ʈ�����ϴ� �ڵ� �߰�
        // ���� ���, UI�� Ȱ��ȭ�ϰų�, ���ο� ���� �����ϰ� �ϰų� �ϴ� ���� �۾�
    }

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
