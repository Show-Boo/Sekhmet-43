using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class FinalCutscene : MonoBehaviour
{
    private AudioSource[] audioSources;  // 모든 오디오 소스를 저장할 배열
    public GameObject crosshair;  // 크로스헤어 UI 오브젝트

    public GameObject Black;

    void Start()
    {
        VideoPlayer cutscenePlayer = GetComponent<VideoPlayer>();
        // 씬에 있는 모든 AudioSource를 가져와 배열에 저장
        audioSources = FindObjectsOfType<AudioSource>();

        // 컷씬 시작과 끝 이벤트 등록
        cutscenePlayer.started += MuteAllAudioSources;
        cutscenePlayer.loopPointReached += UnmuteAllAudioSources;

        // 컷씬 시작과 끝에 크로스헤어 UI 제어 추가
        cutscenePlayer.started += HideCrosshair;
        cutscenePlayer.loopPointReached += ShowCrosshair;
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
        /*
        foreach (AudioSource audio in audioSources)
        {
            audio.enabled = true;  // AudioSource 활성화
        }
        */

        Black.SetActive(true);
    }

    // 크로스헤어를 숨기는 함수
    void HideCrosshair(VideoPlayer vp)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }

    }

    // 크로스헤어를 다시 표시하는 함수
    void ShowCrosshair(VideoPlayer vp)
    {
        /*
        if (crosshair != null)
        {
            crosshair.SetActive(true);
        }
        */

        OnEndingCutsceneComplete();
    }

    void OnEndingCutsceneComplete()
    {
        //yield return new WaitForSeconds(0.001f); // 엔딩시 잠시 대기 -> 그냥 제거
        Application.Quit(); // 바로 종료

        // 에디터에서 테스트할 때는 EditorApplication 종료...
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
