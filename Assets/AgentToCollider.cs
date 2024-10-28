using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentToCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform targetTransform; // �̵��� ��ǥ ��ġ�� �ִ� Collider�� Transform
    private EnemyMove EnemyMove;

    private void Start()
    {
        EnemyMove = agent.GetComponent<EnemyMove>();
    }

    void Update()
    {
        
            if (EnemyMove.PlayerDead && !EnemyMove.restart)
            {
                agent.Warp(targetTransform.position); // ��ǥ ��ġ�� ��� �� �� �̵�
                EnemyMove.restart = true;
            }
        
        
    }
}
