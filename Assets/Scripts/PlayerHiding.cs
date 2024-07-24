using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo 1. Ȱ��ȭ �� hiding ī�޶� �����̱� -> done
//Todo 2. �����ٰ� ���� -> done
//3. �ٰŸ������� ��� ���� -> done �ٵ� player �������϶��� ���� ������ �ȿ�
//3-2 �������϶��� player ��ġ�� ����
//4. ���� ���� ���Ŀ��� �������� �ʰ�
//5. ���� ��ġ�� �ι� �̻� ���� �� �ְ� : ī�޶� ��Ȱ��ȭ �Ǿ �����ϰ� or ��Ȱ��ȭ �ȵǰ�,,,,,,,,,,,,,,,,,, -> done


//0717 : å�� �� ���� q ������ ���忡 ������,,������ �� ��, ���ư��� �͵� �ȵ�, -> �ʱ⿡ ��� ī�޶� Ȱ��ȭ�Ǿ��ִ� ſ�ε� -> �ʱ� ��Ȱ��ȭ�� �ذ���
// : ���常 no rendering camera ��� ��..����
// : ī�޶� ���� üũ�ڽ��� üũ�ߴ��� ���� �ذ᤾��

public class PlayerHiding : MonoBehaviour
{
    private Camera CurrentCamera;
    private Camera previousCamera; // ���� ī�޶� ������ ����

    public bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾� ����

    private EnemyMove[] enemyMove;

    //Raycast
    public Camera playerCamera; // �÷��̾��� ī�޶�
    public float checkDistance = 5.0f; // üũ�� �Ÿ�
    public LayerMask layerMask; // �浹�� ������ ���̾� ����ũ
    public string interactableTag = "InteractiveObject"; // ��ȣ�ۿ��� �±�

    void Start()
    {
        enemyMove = FindObjectsOfType<EnemyMove>();

        previousCamera = playerCamera;
    }

    void Update()
    {
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

                    CurrentCamera = hit.collider.GetComponentInChildren<Camera>(true); // true �����ϸ� ��Ȱ��ȭ�� ��ü�� ����

                    if (CurrentCamera != null)
                    {
                        Debug.Log("CurrentCamera: " + CurrentCamera.name);

                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            // QŰ�� ������ ī�޶� ��ȯ
                            SwitchCamera();
                        }
                    }

                    else//�ι�°���ʹ� ī�޶� ������ ����..��???????? -> ��Ȱ��ȭ �� ī�޶�� ã�Ƴ��� ����
                    {
                        Debug.Log("There is no Camera on the interactable object.");
                    }
                }
            }

            else
            {
                CurrentCamera = null;
                // ���� �Ÿ� ���� ������ ��ü�� ���� ��
                Debug.Log("No interactable object within range.");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (previousCamera != null)
                {
                    CurrentCamera = previousCamera;
                    SwitchCamera();
                }
                else
                {
                    Debug.LogError("Previous camera is null!");
                }
            }
        }
    }

    void SwitchCamera()
    {
        if (CurrentCamera == null)
        {
            Debug.LogError("CurrentCamera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)
        {
            // �ٸ� ������Ʈ�� ī�޶� Ȱ��ȭ�ϰ� �÷��̾� ī�޶� ��Ȱ��ȭ
            if (CurrentCamera != null)
            {
                CurrentCamera.gameObject.SetActive(true);
                

                Debug.Log("Switched to CurrentCamera: " + CurrentCamera.name);
            }

            playerCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {

                enemyMoveScript.ActivatedCamera = CurrentCamera;

            }

            
            previousCamera = CurrentCamera;

        }
        else
        {
            // �÷��̾� ī�޶� Ȱ��ȭ�ϰ� �ٸ� ������Ʈ�� ī�޶� ��Ȱ��ȭ

            playerCamera.gameObject.SetActive(true);

            if (previousCamera != null)
            {

                previousCamera.gameObject.SetActive(false);
                Debug.Log("Switched to PlayerCamera.");

            }

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = playerCamera;
            }

            previousCamera = playerCamera;
        }

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;

        Debug.Log("isPlayer1Active: " + isPlayer1Active);
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * checkDistance);

        }
    }
}