using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    public int playerRoomID = -1;

    private Camera CurrentCamera;
    private Camera previousCamera; // ���� ī�޶� ������ ����

    public bool isPlayer1Active = true; // ���� Ȱ��ȭ�� �÷��̾� ����

    public Ray playerRay; //player���� ������ Ray

    private EnemyMove[] enemyMove;

    public GameObject player;

    //Raycast
    public Camera playerCamera; // �÷��̾��� ī�޶�
    public float checkDistance = 0.5f; // üũ�� �Ÿ�
    public LayerMask layerMask; // �浹�� ������ ���̾� ����ũ
    public string interactableTag = "InteractiveObject"; // ��ȣ�ۿ��� �±�

    public Image crosshair;

    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;

    public AudioClip soundClip; // ����� �Ҹ� �޾ƿ�
    private AudioSource audioSource;

    public bool isBeating = false;

    public bool HeartBeatPlaying = false;
    void Start()
    {
        enemyMove = FindObjectsOfType<EnemyMove>();

        previousCamera = playerCamera;

        // AudioSource ������Ʈ�� �����ɴϴ�.
        audioSource = GetComponent<AudioSource>();
        // �Ҹ� Ŭ���� �����մϴ�.
        audioSource.clip = soundClip;
        // Play On Awake �ɼ��� ���ϴ� (���� ���� �� �ڵ� ��� ����)
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isPlayer1Active)
        {
            playerRay = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(playerRay, out hit, checkDistance, layerMask))
            {
                // ���� �Ÿ� ���� ������ ��ü�� �����Ǹ�
                if (hit.collider.CompareTag(interactableTag))
                {
                    Debug.Log("Interactable object within range: " + hit.collider.name);

                    CurrentCamera = hit.collider.GetComponentInChildren<Camera>(true); // true �����ϸ� ��Ȱ��ȭ�� ��ü�� ����
                    crosshair.color = crosshairHoverColor;

                    if (CurrentCamera != null)
                    {
                        //Debug.Log("CurrentCamera: " + CurrentCamera.name);

                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            // QŰ�� ������ ī�޶� ��ȯ
                            SwitchCamera();
                            crosshair.color = crosshairNormalColor;
                        }
                    }

                    else//�ι�°���ʹ� ī�޶� ������ ����..��???????? -> ��Ȱ��ȭ �� ī�޶�� ã�Ƴ��� ����
                    {
                        //Debug.Log("There is no Camera on the interactable object.");
                    }
                }
            }

            else
            {
                CurrentCamera = null;
                // ���� �Ÿ� ���� ������ ��ü�� ���� ��
                //Debug.Log("No interactable object within range.");
            }
        }
        else//��������
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (previousCamera != null)
                {
                    CurrentCamera = previousCamera;//���� -> ���� ī�޶�� �־��ֱ�
                    SwitchCamera();
                }
                else
                {
                    Debug.LogError("Previous camera is null!");
                }
            }
        }

        if (isBeating)
        {
            //PlaySound();
        }

        if (HeartBeatPlaying)
        {
            PlaySound();
            Debug.Log("Play");
        }
        else
        {
            endSound();

            Debug.Log("end");
        }

    }

    void SwitchCamera()
    {
        if (CurrentCamera == null)
        {
            Debug.LogError("CurrentCamera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)//������
        {
            // �ٸ� ������Ʈ�� ī�޶� Ȱ��ȭ�ϰ� �÷��̾� ī�޶� ��Ȱ��ȭ
            if (CurrentCamera != null)
            {
                CurrentCamera.gameObject.SetActive(true);
                AudioListener newListener = CurrentCamera.GetComponent<AudioListener>();//������
                newListener.enabled = true;
                //Debug.Log("Switched to CurrentCamera: " + CurrentCamera.name);
            }
            AudioListener nowListener = playerCamera.GetComponent<AudioListener>();
            playerCamera.gameObject.SetActive(false);
            player.SetActive(false);
            nowListener.enabled = false;
            
            
            foreach (var enemyMoveScript in enemyMove)
            {

                enemyMoveScript.ActivatedCamera = CurrentCamera;

            }
            

            
            previousCamera = CurrentCamera;

        }
        else//��������Q�� ������
        {
            // �÷��̾� ī�޶� Ȱ��ȭ�ϰ� �ٸ� ������Ʈ�� ī�޶� ��Ȱ��ȭ

            playerCamera.gameObject.SetActive(true);
            player.SetActive(true);

            AudioListener newListener = playerCamera.GetComponent<AudioListener>();//������
            newListener.enabled = true;

            if (previousCamera != null)
            {

                previousCamera.gameObject.SetActive(false);
                //Debug.Log("Switched to PlayerCamera.");
                AudioListener nowListener = previousCamera.GetComponent<AudioListener>();
                nowListener.enabled = false;
            }
            
            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = playerCamera;
            }
            

            previousCamera = playerCamera;
        }

        // Ȱ��ȭ�� �÷��̾� ���� ������Ʈ
        isPlayer1Active = !isPlayer1Active;

        //Debug.Log("isPlayer1Active: " + isPlayer1Active);
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * checkDistance);

            //Debug.Log("checkdistance is " + checkDistance);

        }
    }

    void PlaySound()
    {
        // �Ҹ��� �̹� ��� ���� �ƴϸ� ����մϴ�.
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void endSound()
    {
        audioSource.Stop();
    }


}