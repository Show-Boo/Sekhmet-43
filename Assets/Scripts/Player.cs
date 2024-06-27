using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform deskTransform; // 책상 Transform
    public Transform Y;
    public MeshCollider YCollider; // 책상 mesh
    public MeshCollider deskFloorCollider; // 책상 아래 바닥 Collider
    public BoxCollider underDeskCollider; // 책상 아래 위치에 설정된 Box Collider
    public float underDeskOffset = 1.0f; // 책상 아래로 이동할 때의 오프셋
    public bool isUnderDesk = false;

    private Vector3 originalPosition;



    void Start()
    {
        // 시작 시 책상 아래 Collider를 비활성화
        underDeskCollider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isUnderDesk)
            {
                // 원래 위치로 돌아가기
                transform.position = originalPosition;

                isUnderDesk = false;


                StartCoroutine(Coroutine());
                /*
                // 책상 아래 Collider 비활성화
                underDeskCollider.gameObject.SetActive(false);

                // 책상 아래 바닥 Collider 활성화
                deskFloorCollider.enabled = true;

                //YCollider.gameObject.SetActive(true);
                Y.GetComponent<BoxCollider>().enabled = true;
                */


            }

            else
            {
                // 원래 위치 저장
                originalPosition = transform.position;

                // 책상 아래 바닥 Collider 비활성화
                deskFloorCollider.enabled = false;

                // 책상 아래 Collider 활성화
                underDeskCollider.gameObject.SetActive(true);

                //YCollider.gameObject.SetActive(false);
                Y.GetComponent<BoxCollider>().enabled = false;

                // 플레이어를 책상 아래로 이동
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

        // 책상 아래 바닥 Collider 활성화
        deskFloorCollider.enabled = true;

        //YCollider.gameObject.SetActive(true);
        Y.GetComponent<BoxCollider>().enabled = true;

        yield break;
    }

}

