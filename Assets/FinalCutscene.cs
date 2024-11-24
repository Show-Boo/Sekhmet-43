using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class FinalCutscene : MonoBehaviour
{
    private AudioSource[] audioSources;  // ��� ����� �ҽ��� ������ �迭
    public GameObject crosshair;  // ũ�ν���� UI ������Ʈ

    void Start()
    {
        VideoPlayer cutscenePlayer = GetComponent<VideoPlayer>();
        // ���� �ִ� ��� AudioSource�� ������ �迭�� ����
        audioSources = FindObjectsOfType<AudioSource>();

        // �ƾ� ���۰� �� �̺�Ʈ ���
        cutscenePlayer.started += MuteAllAudioSources;
        cutscenePlayer.loopPointReached += UnmuteAllAudioSources;

        // �ƾ� ���۰� ���� ũ�ν���� UI ���� �߰�
        cutscenePlayer.started += HideCrosshair;
        cutscenePlayer.loopPointReached += ShowCrosshair;
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

    // ũ�ν��� ����� �Լ�
    void HideCrosshair(VideoPlayer vp)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }
        StartCoroutine(OnEndingCutsceneComplete());
    }

    // ũ�ν��� �ٽ� ǥ���ϴ� �Լ�
    void ShowCrosshair(VideoPlayer vp)
    {
        if (crosshair != null)
        {
            crosshair.SetActive(true);
        }
    }

    public IEnumerator OnEndingCutsceneComplete()
    {
        yield return new WaitForSeconds(3); // ������ 5�� ���
        Application.Quit(); // �ٷ� ����

        // �����Ϳ��� �׽�Ʈ�� ���� EditorApplication ����...
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
