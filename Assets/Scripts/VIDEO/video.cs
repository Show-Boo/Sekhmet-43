using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlayer;
    public AudioSource backgroundMusic;
    public int timeToStop;
    private bool videoPlayed = false;
    private bool musicPaused = false; // ��� ���� �Ͻ� ���� ���θ� �����ϴ� ����

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
            // ��� ���� �Ͻ� ����
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

    // ���� ���� �Լ�
    void StopVideo()
    {
        videoPlayer.SetActive(false);
        // ��� ���� �ٽ� ���
        if (musicPaused)
        {
            backgroundMusic.UnPause(); // ��� ���� �簳
            musicPaused = false;
        }
        else
        {
            backgroundMusic.Play(); // ��� ���� ���
        }
    }
}
