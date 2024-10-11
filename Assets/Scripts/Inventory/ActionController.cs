using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range = 5.0f; // 습득 가능한 최대 거리
    [SerializeField] private LayerMask layerMask; // 아이템 레이어에만 반응
    [SerializeField] private Text actionText;
    [SerializeField] private Inventory theInventory;

    private bool pickupActivated = false; // 습득 가능할 시 true
    private RaycastHit hitInfo; // 충돌체 정보 저장
    private bool hasKeyCard = false; // KeyCard를 먹었는지 여부
    private const string itemTag = "Item"; // 아이템 태그

    private void Start()
    {
        if (actionText == null)
            Debug.LogError("ActionText가 설정되지 않았습니다.");

        if (theInventory == null)
            Debug.LogError("Inventory가 설정되지 않았습니다.");
    }

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    // E 키를 눌러 아이템을 습득하는 로직
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated)
        {
            CanPickUp();
        }
    }

    // 아이템을 습득할 수 있는지 확인
    private void CanPickUp()
    {
        if (pickupActivated && hitInfo.transform != null)
        {
            var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Debug.Log(itemPickUp.item.itemName + " 획득함!!");

                // 태그가 "Item"인 오브젝트를 획득하면 문을 열 수 있도록 설정
                hasKeyCard = true;
                Debug.Log("아이템을 획득했으므로 문을 열 수 있습니다.");

                theInventory.AcquireItem(itemPickUp.item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    // 레이캐스트로 아이템을 감지
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

    // 아이템 정보를 화면에 표시
    private void ItemInfoAppear()
    {
        pickupActivated = true;

        if (actionText != null)
        {
            actionText.gameObject.SetActive(true);
            var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                actionText.text = itemPickUp.item.itemName + " 획득 " + "<color=yellow>{E}</color>";
            }
        }
    }

    // 아이템 정보 숨김
    private void InfoDisappear()
    {
        pickupActivated = false;

        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }

    // 문을 열 수 있는지 확인하는 메서드 (다른 스크립트에서 접근)
    public bool CanOpenDoor()
    {
        return hasKeyCard;
    }
}
