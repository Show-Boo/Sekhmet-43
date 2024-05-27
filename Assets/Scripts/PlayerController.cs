using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform deskTransform; // å�� Transform
    public Transform Y;
    public MeshCollider deskFloorCollider; // å�� �Ʒ� �ٴ� Collider
    public BoxCollider underDeskCollider; // å�� �Ʒ� ��ġ�� ������ Box Collider
    public float underDeskOffset = 1.0f; // å�� �Ʒ��� �̵��� ���� ������
    public bool isUnderDesk = false;

    private Vector3 originalPosition;



    void Start()
    {
        // ���� �� å�� �Ʒ� Collider�� ��Ȱ��ȭ
        underDeskCollider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isUnderDesk)
            {
                // ���� ��ġ�� ���ư���
                transform.position = originalPosition;
                isUnderDesk = false;

                // å�� �Ʒ� Collider ��Ȱ��ȭ
                underDeskCollider.gameObject.SetActive(false);

                // å�� �Ʒ� �ٴ� Collider Ȱ��ȭ
                deskFloorCollider.enabled = true;
            }

            else
            {
                // ���� ��ġ ����
                originalPosition = transform.position;

                // å�� �Ʒ� �ٴ� Collider ��Ȱ��ȭ
                deskFloorCollider.enabled = false;

                // å�� �Ʒ� Collider Ȱ��ȭ
                underDeskCollider.gameObject.SetActive(true);

                // �÷��̾ å�� �Ʒ��� �̵�
                Vector3 underDeskPosition = new Vector3(deskTransform.position.x, Y.position.y + 0.7f, deskTransform.position.z);
                transform.position = underDeskPosition - deskTransform.forward * underDeskOffset;

                isUnderDesk = true;
            }
        }
    }

}

