using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //üũ����Ʈ �������� �� Ȯ���ؾ���
    //�÷��̾ �������� Ȯ���ؾ��� -> ���� �غ���


    [SerializeField] GameObject player;

    //[SerializeField] List<GameObject> checkPoints;//�̰� �� �ʿ�����?

    [SerializeField] Vector3 vectorPoint;

    [SerializeField] float dead;//�̰� �� ������?

    public bool restart = false;

    private PlayerHiding PlayerHiding;
    //public EnemyMove EnemyMove;//enemy�� ��������..
    

    private void Start()
    {
        PlayerHiding = GetComponent<PlayerHiding>();//player���� ��������,,�� �Ⱦ���
    }

    void Update()
    {
        if (restart)//���ư��� ����.
        {
            //player.transform.position = vectorPoint;//���ư�
            player.transform.position = vectorPoint;

            if (!PlayerHiding.isPlayer1Active)
            {
                PlayerHiding.SwitchCamera();//���ٷ�̤� -> ����!
            }

            restart = false;
            
            Debug.Log("move player");
            /*
            foreach (var enemyMoveScript in EnemyMove)
            {
                enemyMoveScript.isPlayerDead = false;//�ٽ� ���� �簳
            }
            */
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = other.transform.position;
            //Destroy(other.gameObject);
        }
    }
}
