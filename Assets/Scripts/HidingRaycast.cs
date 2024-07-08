using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingRaycast : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera playerCamera; // �÷��̾��� ī�޶�
    public float checkDistance = 10.0f; // üũ�� �Ÿ�
    public LayerMask layerMask; // �浹�� ������ ���̾� ����ũ
    public string interactableTag = "InteractiveObject"; // ��ȣ�ۿ��� �±�

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, checkDistance, layerMask))
        {
            // ���� �Ÿ� ���� ������ ��ü�� �����Ǹ�
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Interactable object within range: " + hit.collider.name);
                // ���⼭ �۾� ����
            }
        }

        else
        {
            // ���� �Ÿ� ���� ������ ��ü�� ���� ��
            Debug.Log("No interactable object within range.");
        }

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
