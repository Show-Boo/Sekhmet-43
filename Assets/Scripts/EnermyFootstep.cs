using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public Transform player; // 플레이어의 위치
    private AudioSource audioSource; // 적의 발소리
    public float maxDistance = 30f; // 플레이어와 적 사이의 최대 거리
    public float minPitch = 0.1f; // 최소 피치 (멀리 있을 때)
    public float maxPitch = 4.0f; // 최대 피치 (가까이 있을 때)
    public float minVolume = 0.1f; // 최소 볼륨 (멀리 있을 때)
    public float maxVolume = 1.0f; // 최대 볼륨 (가까이 있을 때)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 플레이어와 적 사이의 거리 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 거리에 따라 피치와 볼륨 조정
        float t = Mathf.Clamp01(1 - (distanceToPlayer / maxDistance)); // 거리를 0에서 1 사이 값으로 변환
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, t); // 피치 조정
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t); // 볼륨 조정

        // 플레이어가 일정 거리 안에 있을 때만 발소리 재생
        if (!audioSource.isPlaying && distanceToPlayer <= maxDistance)
        {
            audioSource.Play();
        }
        else if (distanceToPlayer > maxDistance)
        {
            audioSource.Stop(); // 플레이어가 멀어지면 소리 멈춤
        }
    }
}
