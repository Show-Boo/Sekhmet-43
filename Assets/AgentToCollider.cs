using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AgentToCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform targetTransform; // 이동할 목표 위치가 있는 Collider의 Transform
    public Transform targetTransform2; //두번째 transform
    public GameObject player;
    private EnemyMove EnemyMove;

    private void Start()
    {
        EnemyMove = agent.GetComponent<EnemyMove>();
    }

    void Update()
    {
        
        if (EnemyMove.PlayerDead && !EnemyMove.retry)
        {
            if (Vector3.Distance(player.transform.position, targetTransform.position) <= Vector3.Distance(player.transform.position, targetTransform2.position))
            {
                agent.Warp(targetTransform2.position); // 목표 위치로 즉시 한 번 이동
            }
            else
            {
                agent.Warp(targetTransform.position); //이동
            }
            

            EnemyMove.retry = true;

            }
        
        
    }
}
