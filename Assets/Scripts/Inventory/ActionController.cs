using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range = 5.0f; // 습득 가능한 최대 거리, 기본값 설정
    private bool pickupActivated = false; // 습득 가능할 시 true
    private RaycastHit hitInfo; // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;

    private void Start()
    {
        // 필요한 컴포넌트들이 할당되어 있는지 체크
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
            // Null 체크 추가
            var itemPickUp = hitInfo.transform?.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Debug.Log(itemPickUp.item.itemName + " 획득함!!");
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

        // Null 체크 추가
        if (actionText != null)
        {
            actionText.gameObject.SetActive(true);

            // Null 체크 추가
            var itemPickUp = hitInfo.transform?.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                actionText.text = itemPickUp.item.itemName + " 획득 " + "<color=yellow>{E}</color>";
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

        // Null 체크 추가
        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }
}
