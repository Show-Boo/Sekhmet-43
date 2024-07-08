using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public Transform[] objects;

    public Transform Player1; // Player1�� Transform
    public Transform HidePlayer; // Player2�� Transform
    public Transform HidePlayer2;

    private Transform CurrentTransform;

    private Camera PlayerCamera; // Player1�� ī�޶�
    private Camera HidePlayerCamera; // Player2�� ī�޶�
    private Camera HidePlayer2Camera;

    private Camera CurrentCamera;
    
    public float switchDistance = 5.0f; // �Ÿ� �Ӱ谪
    public bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾� ����

    //private Enemy enemyMove;

    private Enemy[] enemyMove;
    void Start()
    {
        // ���� �� Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
        HidePlayer2Camera = HidePlayer2.GetComponentInChildren<Camera>();

        
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);
        HidePlayer2Camera.gameObject.SetActive(false);

        enemyMove = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        // �÷��̾� �� �Ÿ� ���

        float distance = float.MaxValue;

        // ��� ��ü���� �Ÿ��� ��
        foreach (Transform obj in objects)
        {
            float currentDistance = Vector3.Distance(Player1.position, obj.position);

            if (currentDistance < distance)
            {
                distance = currentDistance;
                CurrentTransform = obj;
            }
        }
        //float distance = Vector3.Distance(Player1.position, HidePlayer.position);

        if (Input.GetKeyDown(KeyCode.R) && distance <= switchDistance)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // null üũ
        CurrentCamera = CurrentTransform.GetComponent<Camera>();//GetComponentInChildren�ϸ� ���޾ƿͼ� �� obj�� ī�޶� �־���

        /*
        if (CurrentTransform != null)
        {
            Debug.LogError(CurrentTransform);
            return;
        }
        ��������
        */

        if (CurrentCamera == null)
        {
            Debug.LogError("CurrentCamera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)
        {
            // Player2 ī�޶� Ȱ��ȭ, Player1 ī�޶� ��Ȱ��ȭ
            CurrentCamera.gameObject.SetActive(true);//Ȱ��ȭ���� �����ֱ�.. ������ �𸣰ڴµ� �׷��� ������ ����
            PlayerCamera.gameObject.SetActive(false);
            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;
            }

        }
        else
        {
            // Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
            PlayerCamera.gameObject.SetActive(true);
            CurrentCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;
            }

        }

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;
        Debug.Log("isPlayer1Active: " + isPlayer1Active);

    }
}



