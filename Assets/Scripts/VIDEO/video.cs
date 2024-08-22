using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlayer; // 비디오 플레이어 오브젝트
    public int timeToStop; // 비디오를 중지할 시간
    private bool videoPlayed = false; // 비디오가 재생되었는지 여부를 추적하는 변수

    void Start()
    {
        videoPlayer.SetActive(false); // 비디오 플레이어를 비활성화 상태로 시작
    }

    void OnTriggerEnter(Collider player)
    {
        // 플레이어가 트리거에 진입하고, 비디오가 아직 재생되지 않았을 때
        if (player.gameObject.tag == "Player" && !videoPlayed)
        {
            videoPlayer.SetActive(true); // 비디오 플레이어 활성화
            Invoke("StopVideo", timeToStop); // 일정 시간 후 비디오 중지 함수 호출
            videoPlayed = true; // 비디오가 재생되었음을 기록
        }
    }

    // 비디오 중지 함수
    void StopVideo()
    {
        videoPlayer.SetActive(false); // 비디오 플레이어 비활성화
        // 추가적으로, 필요시 게임 화면으로 돌아가는 로직을 여기에 추가할 수 있음
    }
}
