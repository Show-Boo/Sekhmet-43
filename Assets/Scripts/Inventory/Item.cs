using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "NewItem/item")]
public class Item : ScriptableObject
{

    public string itemName; //������ �̸�
    public Sprite itemImage; //������ �̹���
    public ItemType itemType; //������ ����
    public GameObject itemPrefab;


    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

}

