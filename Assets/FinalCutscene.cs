using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    public AudioMixer gameAudioMixer;         // AudioMixer�� �Ҵ�
    private float originalMasterVolume = 0f;  // ���� Master ���� ��

    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();//��ü ������Ʈ���� ��������
        // ���� Master ���� ���� ����
        gameAudioMixer.GetFloat("Volume (of Master)", out originalMasterVolume);

        // �ƾ� ���۰� �� �̺�Ʈ ���
        videoPlayer.started += MuteMasterAudio;
        videoPlayer.loopPointReached += UnmuteMasterAudio;
    
    }

    // Master ����� ���Ұ� �Լ�
    void MuteMasterAudio(VideoPlayer vp)
    {
        Debug.Log("Muting Master Volume");
        gameAudioMixer.SetFloat("Volume (of Master)", -80f);  // Master ������ -80dB�� ���� ���Ұ�
    }

    // Master ����� ���� �Լ�
    void UnmuteMasterAudio(VideoPlayer vp)
    {
        Debug.Log("Restoring Master Volume");
        gameAudioMixer.SetFloat("Volume (of Master)", originalMasterVolume);  // ���� Master ���� ����
    }
}