using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlayer; // ���� �÷��̾� ������Ʈ
    public int timeToStop; // ������ ������ �ð�
    private bool videoPlayed = false; // ������ ����Ǿ����� ���θ� �����ϴ� ����

    void Start()
    {
        videoPlayer.SetActive(false); // ���� �÷��̾ ��Ȱ��ȭ ���·� ����
    }

    void OnTriggerEnter(Collider player)
    {
        // �÷��̾ Ʈ���ſ� �����ϰ�, ������ ���� ������� �ʾ��� ��
        if (player.gameObject.tag == "Player" && !videoPlayed)
        {
            videoPlayer.SetActive(true); // ���� �÷��̾� Ȱ��ȭ
            Invoke("StopVideo", timeToStop); // ���� �ð� �� ���� ���� �Լ� ȣ��
            videoPlayed = true; // ������ ����Ǿ����� ���
        }
    }

    // ���� ���� �Լ�
    void StopVideo()
    {
        videoPlayer.SetActive(false); // ���� �÷��̾� ��Ȱ��ȭ
        // �߰�������, �ʿ�� ���� ȭ������ ���ư��� ������ ���⿡ �߰��� �� ����
    }
}
