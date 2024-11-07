using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AgentToCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform targetTransform; // �̵��� ��ǥ ��ġ�� �ִ� Collider�� Transform
    public Transform targetTransform2; //�ι�° transform
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
                agent.Warp(targetTransform2.position); // ��ǥ ��ġ�� ��� �� �� �̵�
            }
            else
            {
                agent.Warp(targetTransform.position); //�̵�
            }
            

            EnemyMove.retry = true;

            }
        
        
    }
}
