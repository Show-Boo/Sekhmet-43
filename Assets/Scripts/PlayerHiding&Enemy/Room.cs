using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomID; // ���� ID

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ �濡 ����
            PlayerHiding player = other.GetComponent<PlayerHiding>();

            if (player != null)
            {
                player.playerRoomID = roomID;
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            // ���� �濡 ����
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.EnemyRoomID = roomID;
            }

            Debug.Log("EnemyTag");
        }
    }
}
