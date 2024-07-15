using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo 1. Ȱ��ȭ �� hiding ī�޶� �����̱�
//Todo 2. �����ٰ� ����
//3. �ٰŸ������� ��� ����
//4. ���� ���� ���Ŀ��� �������� �ʰ�
//5.

public class PlayerHiding : MonoBehaviour
{
    //public Transform[] objects;

    //public Transform Player1; // Player1�� Transform
    //public Transform HidePlayer; // Player2�� Transform
    //public Transform HidePlayer2;

    //private Transform CurrentTransform;

    //private Camera PlayerCamera; // Player1�� ī�޶�
    //private Camera HidePlayerCamera; // Player2�� ī�޶�
    //private Camera HidePlayer2Camera;

    private Camera CurrentCamera;
    
    //public float switchDistance = 5.0f; // �Ÿ� �Ӱ谪
    public bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾� ����

    private Enemy[] enemyMove;

    //Raycast
    public Camera playerCamera; // �÷��̾��� ī�޶�
    public float checkDistance = 5.0f; // üũ�� �Ÿ�
    public LayerMask layerMask; // �浹�� ������ ���̾� ����ũ
    public string interactableTag = "InteractiveObject"; // ��ȣ�ۿ��� �±�

    //private Camera targetCamera;

    void Start()
    {
        // ���� �� Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
        /*
        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
        HidePlayer2Camera = HidePlayer2.GetComponentInChildren<Camera>();

        
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);
        HidePlayer2Camera.gameObject.SetActive(false);
        */

        enemyMove = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        // �÷��̾� �� �Ÿ� ���
        /*
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
        */
        if (isPlayer1Active)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, checkDistance, layerMask))
            {
                // ���� �Ÿ� ���� ������ ��ü�� �����Ǹ�
                if (hit.collider.CompareTag(interactableTag))
                {
                    Debug.Log("Interactable object within range: " + hit.collider.name);

                    CurrentCamera = hit.collider.GetComponentInChildren<Camera>();

                    if (CurrentCamera != null && Input.GetKeyDown(KeyCode.Q))
                    {
                        // QŰ�� ������ ī�޶� ��ȯ
                        SwitchCamera();

                    }

                    else if (CurrentCamera == null)
                    {

                        Debug.Log("There is no Camera");

                    }
                }
            }

            else
            {
                // ���� �Ÿ� ���� ������ ��ü�� ���� ��
                Debug.Log("No interactable object within range.");
            }


        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Q)) { 

                SwitchCamera();
            }
        }
        
    }

    void SwitchCamera()
    {
        // null üũ
        //CurrentCamera = CurrentTransform.GetComponent<Camera>();//GetComponentInChildren�ϸ� ���޾ƿͼ� �� obj�� ī�޶� �־���

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
            playerCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;//enemy�� Ÿ�� �ٲ��ֱ�
            }
        }

        else
        {
            // Player1 ī�޶� Ȱ��ȭ, Player2 ī�޶� ��Ȱ��ȭ
            playerCamera.gameObject.SetActive(true);
            CurrentCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = playerCamera;//enemy�� Ÿ�� �ٲ��ֱ�
            }

        }
        /*
        foreach (var enemyMoveScript in enemyMove)
        {
            enemyMoveScript.ActivatedCamera = CurrentCamera;//enemy�� Ÿ�� �ٲ��ֱ�
        }
        */

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;

        Debug.Log("isPlayer1Active: " + isPlayer1Active);

    }

    void OnDrawGizmos()
    {
        // ����׸� ���� ����ĳ��Ʈ ��θ� �� �信 �׷���
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * checkDistance);
        }

    }
}



