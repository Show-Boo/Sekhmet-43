using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public Transform player;
    private AudioSource audioSource;
    public float maxDistance = 30f;
    public float maxVerticalDistance = 5f; // 층간 거리 제한을 위한 y축 최대 거리
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
        float verticalDistance = Mathf.Abs(player.position.y - transform.position.y); // 수직 거리 계산

        // y축 거리 제한을 추가하여 같은 층에 있을 때만 발소리 재생
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
            audioSource.Stop(); // 플레이어가 다른 층에 있을 때 발소리 멈춤
        }
    }
}
