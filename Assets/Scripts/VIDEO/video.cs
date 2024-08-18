using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlayer;
    public AudioSource backgroundMusic;
    public int timeToStop;
    private bool videoPlayed = false;
    private bool musicPaused = false; // 배경 음악 일시 정지 여부를 추적하는 변수

    // Use this for initialization
    void Start()
    {
        videoPlayer.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && !videoPlayed)
        {
            // 배경 음악 일시 정지
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
                musicPaused = true;
            }

            videoPlayer.SetActive(true);
            Invoke("StopVideo", timeToStop);

            videoPlayed = true;
        }
    }

    // 비디오 중지 함수
    void StopVideo()
    {
        videoPlayer.SetActive(false);
        // 배경 음악 다시 재생
        if (musicPaused)
        {
            backgroundMusic.UnPause(); // 배경 음악 재개
            musicPaused = false;
        }
        else
        {
            backgroundMusic.Play(); // 배경 음악 재생
        }
    }
}
