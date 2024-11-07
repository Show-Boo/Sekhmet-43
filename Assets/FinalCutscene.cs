using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    public AudioMixer gameAudioMixer;         // AudioMixer를 할당
    private float originalMasterVolume = 0f;  // 원래 Master 볼륨 값

    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//자체 오브젝트에서 가져오기
        // 원래 Master 볼륨 값을 저장
        gameAudioMixer.GetFloat("Volume (of Master)", out originalMasterVolume);

        // 컷씬 시작과 끝 이벤트 등록
        videoPlayer.started += MuteMasterAudio;
        videoPlayer.loopPointReached += UnmuteMasterAudio;
    
    }

    // Master 오디오 음소거 함수
    void MuteMasterAudio(VideoPlayer vp)
    {
        Debug.Log("Muting Master Volume");
        gameAudioMixer.SetFloat("Volume (of Master)", -80f);  // Master 볼륨을 -80dB로 낮춰 음소거
    }

    // Master 오디오 복구 함수
    void UnmuteMasterAudio(VideoPlayer vp)
    {
        Debug.Log("Restoring Master Volume");
        gameAudioMixer.SetFloat("Volume (of Master)", originalMasterVolume);  // 원래 Master 볼륨 복구
    }
}