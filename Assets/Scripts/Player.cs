using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform deskTransform; // å�� Transform
    public Transform Y;
    public MeshCollider YCollider; // å�� mesh
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


                StartCoroutine(Coroutine());
                /*
                // å�� �Ʒ� Collider ��Ȱ��ȭ
                underDeskCollider.gameObject.SetActive(false);

                // å�� �Ʒ� �ٴ� Collider Ȱ��ȭ
                deskFloorCollider.enabled = true;

                //YCollider.gameObject.SetActive(true);
                Y.GetComponent<BoxCollider>().enabled = true;
                */


            }

            else
            {
                // ���� ��ġ ����
                originalPosition = transform.position;

                // å�� �Ʒ� �ٴ� Collider ��Ȱ��ȭ
                deskFloorCollider.enabled = false;

                // å�� �Ʒ� Collider Ȱ��ȭ
                underDeskCollider.gameObject.SetActive(true);

                //YCollider.gameObject.SetActive(false);
                Y.GetComponent<BoxCollider>().enabled = false;

                // �÷��̾ å�� �Ʒ��� �̵�
                Vector3 underDeskPosition = new Vector3(deskTransform.position.x, deskTransform.position.y, deskTransform.position.z);
                transform.position = underDeskPosition - deskTransform.forward * underDeskOffset;

                isUnderDesk = true;

            }
        }
    }

    IEnumerator Coroutine()
    {

        yield return new WaitForSeconds(0.5f);

        underDeskCollider.gameObject.SetActive(false);

        // å�� �Ʒ� �ٴ� Collider Ȱ��ȭ
        deskFloorCollider.enabled = true;

        //YCollider.gameObject.SetActive(true);
        Y.GetComponent<BoxCollider>().enabled = true;

        yield break;
    }

}

