using UnityEngine;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    private AudioSource[] audioSources;  // 모든 오디오 소스를 저장할 배열
   

    void Start()
    {
        VideoPlayer cutscenePlayer = GetComponent<VideoPlayer>();
        // 씬에 있는 모든 AudioSource를 가져와 배열에 저장
        audioSources = FindObjectsOfType<AudioSource>();

        // 컷씬 시작과 끝 이벤트 등록
        cutscenePlayer.started += MuteAllAudioSources;
        cutscenePlayer.loopPointReached += UnmuteAllAudioSources;
    }

    // 모든 오디오 소스를 비활성화하는 함수
    void MuteAllAudioSources(VideoPlayer vp)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.enabled = false;  // AudioSource 비활성화
        }
    }

    // 모든 오디오 소스를 활성화하는 함수
    void UnmuteAllAudioSources(VideoPlayer vp)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.enabled = true;  // AudioSource 활성화
        }
    }
}
