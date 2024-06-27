using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{

    public Transform Player1; // Player1�� Transform
    public Transform HidePlayer; // Player2�� Transform
    private Camera PlayerCamera; // Player1�� ī�޶�
    private Camera HidePlayerCamera; // Player2�� ī�޶�
    public float switchDistance = 5.0f; // �Ÿ� �Ӱ谪

    public bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾� ����

    void Start()
    {
        // ���� �� Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);

        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // �÷��̾� �� �Ÿ� ���
        float distance = Vector3.Distance(Player1.position, HidePlayer.position);

        if (Input.GetKeyDown(KeyCode.R) && distance <= switchDistance)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isPlayer1Active)
        {
            // Player2 ī�޶� Ȱ��ȭ, Player1 ī�޶� ��Ȱ��ȭ
            PlayerCamera.gameObject.SetActive(false);
            HidePlayerCamera.gameObject.SetActive(true);
        }
        else
        {
            // Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
            PlayerCamera.gameObject.SetActive(true);
            HidePlayerCamera.gameObject.SetActive(false);
        }

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;
    }

}

