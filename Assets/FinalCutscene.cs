using UnityEngine;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    private AudioSource[] audioSources;  // ��� ����� �ҽ��� ������ �迭
   

    void Start()
    {
        VideoPlayer cutscenePlayer = GetComponent<VideoPlayer>();
        // ���� �ִ� ��� AudioSource�� ������ �迭�� ����
        audioSources = FindObjectsOfType<AudioSource>();

        // �ƾ� ���۰� �� �̺�Ʈ ���
        cutscenePlayer.started += MuteAllAudioSources;
        cutscenePlayer.loopPointReached += UnmuteAllAudioSources;
    }

    // ��� ����� �ҽ��� ��Ȱ��ȭ�ϴ� �Լ�
    void MuteAllAudioSources(VideoPlayer vp)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.enabled = false;  // AudioSource ��Ȱ��ȭ
        }
    }

    // ��� ����� �ҽ��� Ȱ��ȭ�ϴ� �Լ�
    void UnmuteAllAudioSources(VideoPlayer vp)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.enabled = true;  // AudioSource Ȱ��ȭ
        }
    }
}
