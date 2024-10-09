using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //üũ����Ʈ �������� �� Ȯ���ؾ���
    //�÷��̾ �������� Ȯ���ؾ��� -> ���� �غ���


    [SerializeField] GameObject player;

    [SerializeField] List<GameObject> checkPoints;//�̰� �� �ʿ�����?

    [SerializeField] Vector3 vectorPoint;

    [SerializeField] float dead;//�̰� �� ������?

    public bool restart = false;


    void Update()
    {
        if (restart)//���ư��� ����.
        {
            //player.transform.position = vectorPoint;//���ư�
            player.transform.position = vectorPoint;
            restart = false;
            Debug.Log("move player");//�굵 �Ǵµ�..
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = other.transform.position;//�׳� tranform�ϸ� �÷��̾��� ��ġ �ݿ���.,.
            Destroy(other.gameObject);//�̰� ����̰� �� ������..
        }
    }
}
