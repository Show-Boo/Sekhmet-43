using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range = 5.0f; // ���� ������ �ִ� �Ÿ�
    [SerializeField] private LayerMask layerMask; // ������ ���̾�� ����
    [SerializeField] private Text actionText;
    [SerializeField] private Inventory theInventory;

    private bool pickupActivated = false; // ���� ������ �� true
    private RaycastHit hitInfo; // �浹ü ���� ����
    private bool hasKeyCard = false; // KeyCard�� �Ծ����� ����
    private const string itemTag = "Item"; // ������ �±�

    private void Start()
    {
        if (actionText == null)
            Debug.LogError("ActionText�� �������� �ʾҽ��ϴ�.");

        if (theInventory == null)
            Debug.LogError("Inventory�� �������� �ʾҽ��ϴ�.");
    }

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    // E Ű�� ���� �������� �����ϴ� ����
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated)
        {
            CanPickUp();
        }
    }

    // �������� ������ �� �ִ��� Ȯ��
    private void CanPickUp()
    {
        if (pickupActivated && hitInfo.transform != null)
        {
            var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Debug.Log(itemPickUp.item.itemName + " ȹ����!!");

                // �±װ� "Item"�� ������Ʈ�� ȹ���ϸ� ���� �� �� �ֵ��� ����
                hasKeyCard = true;
                Debug.Log("�������� ȹ�������Ƿ� ���� �� �� �ֽ��ϴ�.");

                theInventory.AcquireItem(itemPickUp.item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    // ����ĳ��Ʈ�� �������� ����
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.CompareTag(itemTag))
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    // ������ ������ ȭ�鿡 ǥ��
    private void ItemInfoAppear()
    {
        pickupActivated = true;

        if (actionText != null)
        {
            actionText.gameObject.SetActive(true);
            var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                actionText.text = itemPickUp.item.itemName + " ȹ�� " + "<color=yellow>{E}</color>";
            }
        }
    }

    // ������ ���� ����
    private void InfoDisappear()
    {
        pickupActivated = false;

        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }

    // ���� �� �� �ִ��� Ȯ���ϴ� �޼��� (�ٸ� ��ũ��Ʈ���� ����)
    public bool CanOpenDoor()
    {
        return hasKeyCard;
    }
}
