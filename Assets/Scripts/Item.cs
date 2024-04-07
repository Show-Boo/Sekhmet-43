using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "NewItem/item")]
public class Item : ScriptableObject
{

    public string itemName; //아이템 이름
    public Sprite itemImage; //아이템 이미지
    public ItemType itemType; //아이템 유형
    public GameObject itemPrefab;


    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

}

