using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndDoorInteraction : MonoBehaviour
{
    public Item item; // ������ ���� (�÷��̾ �ݴ� ������)
    public Component doorcolliderhere; // ���� ����� �ݶ��̴� ����
    public GameObject keygone; // ���� ������Ʈ ����
    public Inventory playerInventory; // �÷��̾��� �κ��丮

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ �۾� (�ʿ��ϴٸ� �߰�)
    }

    // �÷��̾ Ʈ���� ���� ���� �ӹ��� ���� �� ����
    void OnTriggerStay(Collider other)
    {
        // Ʈ���ſ� ���� ��ü�� �÷��̾����� Ȯ��
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            // �������� �÷��̾��� �κ��丮�� �߰�
            

            // ���� ���� �ݶ��̴� Ȱ��ȭ
            doorcolliderhere.GetComponent<BoxCollider>().enabled = true;

            // ���� ������Ʈ�� ��Ȱ��ȭ (������� ��)
            keygone.SetActive(false);
        }
    }

    // �������� �÷��̾��� �κ��丮�� �߰��ϴ� �޼���
    
}


