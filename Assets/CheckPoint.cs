using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //체크포인트 여러개일 때 확인해야함
    //플레이어가 숨었을때 확인해야함 -> 먼저 해보기


    [SerializeField] GameObject player;

    [SerializeField] List<GameObject> checkPoints;//이게 왜 필요하지?

    [SerializeField] Vector3 vectorPoint;

    [SerializeField] float dead;//이건 왜 적었지?

    public bool restart = false;


    void Update()
    {
        if (restart)//돌아가는 지점.
        {
            //player.transform.position = vectorPoint;//돌아감
            player.transform.position = vectorPoint;
            restart = false;
            Debug.Log("move player");//얘도 되는데..
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = other.transform.position;//그냥 tranform하면 플레이어의 위치 반영됨.,.
            Destroy(other.gameObject);//이걸 재원이가 왜 적었지..
        }
    }
}
