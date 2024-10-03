using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ
    private AudioSource audioSource; // ���� �߼Ҹ�
    public float maxDistance = 30f; // �÷��̾�� �� ������ �ִ� �Ÿ�
    public float minPitch = 0.1f; // �ּ� ��ġ (�ָ� ���� ��)
    public float maxPitch = 4.0f; // �ִ� ��ġ (������ ���� ��)
    public float minVolume = 0.1f; // �ּ� ���� (�ָ� ���� ��)
    public float maxVolume = 1.0f; // �ִ� ���� (������ ���� ��)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // �÷��̾�� �� ������ �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �Ÿ��� ���� ��ġ�� ���� ����
        float t = Mathf.Clamp01(1 - (distanceToPlayer / maxDistance)); // �Ÿ��� 0���� 1 ���� ������ ��ȯ
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, t); // ��ġ ����
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t); // ���� ����

        // �÷��̾ ���� �Ÿ� �ȿ� ���� ���� �߼Ҹ� ���
        if (!audioSource.isPlaying && distanceToPlayer <= maxDistance)
        {
            audioSource.Play();
        }
        else if (distanceToPlayer > maxDistance)
        {
            audioSource.Stop(); // �÷��̾ �־����� �Ҹ� ����
        }
    }
}
