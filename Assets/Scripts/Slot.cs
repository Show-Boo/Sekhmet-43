using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item item; //획득한 아이템.
    public int itemCount; //획득한 아이템 개수
    public Image itemImage; //아이템 이미지

    //필요 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();

    }

}
