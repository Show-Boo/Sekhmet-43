using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q2 : Q
{
    public GameObject item;  // ����Ʈ ������ ������Ʈ
    public override bool cutscene { get; set; } = false;


    public override void UpdateQuest()
    {
        if (item == null)
        {
            CompleteQuest(); // �������� ������ ����Ʈ �Ϸ� ó��
        }
    }

}
