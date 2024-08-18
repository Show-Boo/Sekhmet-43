using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomID; // 방의 ID

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 방에 들어옴
            PlayerHiding player = other.GetComponent<PlayerHiding>();

            if (player != null)
            {
                player.playerRoomID = roomID;
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            // 적이 방에 들어옴
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.EnemyRoomID = roomID;
            }

            Debug.Log("EnemyTag");
        }
    }
}
