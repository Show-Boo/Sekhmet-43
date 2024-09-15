using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range = 5.0f; // ���� ������ �ִ� �Ÿ�, �⺻�� ����
    private bool pickupActivated = false; // ���� ������ �� true
    private RaycastHit hitInfo; // �浹ü ���� ����

    // ������ ���̾�� �����ϵ��� ���̾��ũ ����
    [SerializeField]
    private LayerMask layerMask;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;

    private void Start()
    {
        // �ʿ��� ������Ʈ���� �Ҵ�Ǿ� �ִ��� üũ
        if (actionText == null)
        {
            Debug.Log("ActionText is not assigned in the inspector.");
        }

        if (theInventory == null)
        {
            Debug.LogError("TheInventory is not assigned in the inspector.");
        }
    }

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            // Null üũ �߰�
            var itemPickUp = hitInfo.transform?.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Debug.Log(itemPickUp.item.itemName + " ȹ����!!");
                theInventory.AcquireItem(itemPickUp.item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
            else
            {
                Debug.LogWarning("ItemPickUp component is missing on the object.");
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;

        // Null üũ �߰�
        if (actionText != null)
        {
            actionText.gameObject.SetActive(true);

            // Null üũ �߰�
            var itemPickUp = hitInfo.transform?.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                actionText.text = itemPickUp.item.itemName + " ȹ�� " + "<color=yellow>{E}</color>";
            }
            else
            {
                Debug.LogWarning("ItemPickUp component is missing on the object.");
            }
        }
    }

    private void InfoDisappear()
    {
        pickupActivated = false;

        // Null üũ �߰�
        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }
}
