using System;
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
        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();

        if (PlayerCamera != null)
            PlayerCamera.gameObject.SetActive(true);
        else
            Debug.LogError("Player1 does not have a Camera component!");


        if (HidePlayerCamera != null)
            HidePlayerCamera.gameObject.SetActive(false);
        else
            Debug.LogError("HidePlayer does not have a Camera component!");
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
        // null üũ
        if (PlayerCamera == null || HidePlayerCamera == null)
        {
            Debug.LogError("Camera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)
        {
            // Player2 ī�޶� Ȱ��ȭ, Player1 ī�޶� ��Ȱ��ȭ
            HidePlayerCamera.gameObject.SetActive(true);//Ȱ��ȭ���� �����ֱ�
            PlayerCamera.gameObject.SetActive(false);
        }
        else
        {
            // Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
            PlayerCamera.gameObject.SetActive(true);
            HidePlayerCamera.gameObject.SetActive(false);
        }

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;
        Debug.Log("isPlayer1Active: " + isPlayer1Active);
    }
}


