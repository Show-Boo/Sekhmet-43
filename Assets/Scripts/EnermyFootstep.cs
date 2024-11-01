using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public Transform player;
    private AudioSource audioSource;
    public float maxDistance = 30f;
    public float maxVerticalDistance = 5f; // ���� �Ÿ� ������ ���� y�� �ִ� �Ÿ�
    public float minPitch = 0.1f;
    public float maxPitch = 4.0f;
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        float verticalDistance = Mathf.Abs(player.position.y - transform.position.y); // ���� �Ÿ� ���

        // y�� �Ÿ� ������ �߰��Ͽ� ���� ���� ���� ���� �߼Ҹ� ���
        if (verticalDistance <= maxVerticalDistance)
        {
            float t = Mathf.Clamp01(1 - (distanceToPlayer / maxDistance));
            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, t);
            audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t);

            if (!audioSource.isPlaying && distanceToPlayer <= maxDistance)
            {
                audioSource.Play();
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop(); // �÷��̾ �ٸ� ���� ���� �� �߼Ҹ� ����
        }
    }
}
