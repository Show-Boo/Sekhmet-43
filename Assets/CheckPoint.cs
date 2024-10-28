using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //체크포인트 여러개일 때 확인해야함
    //플레이어가 숨었을때 확인해야함 -> 먼저 해보기


    [SerializeField] GameObject player;

    //[SerializeField] List<GameObject> checkPoints;//이게 왜 필요하지?

    [SerializeField] Vector3 vectorPoint;

    [SerializeField] float dead;//이건 왜 적었지?

    public bool restart = false;

    private PlayerHiding PlayerHiding;
    //public EnemyMove EnemyMove;//enemy가 여러갠데..
    

    private void Start()
    {
        PlayerHiding = GetComponent<PlayerHiding>();//player에서 가져오기,,얘 안쓸듯
    }

    void Update()
    {
        if (restart)//돌아가는 지점.
        {
            //player.transform.position = vectorPoint;//돌아감
            player.transform.position = vectorPoint;

            if (!PlayerHiding.isPlayer1Active)
            {
                PlayerHiding.SwitchCamera();//제바루ㅜㅠ -> 성공!
            }

            restart = false;
            
            Debug.Log("move player");
            /*
            foreach (var enemyMoveScript in EnemyMove)
            {
                enemyMoveScript.isPlayerDead = false;//다시 공격 재개
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
