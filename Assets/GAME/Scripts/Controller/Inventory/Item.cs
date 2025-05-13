using System;
using UnityEngine;
using UnityEngine.UI;
// Eþya sýnýfý
public class Item : MonoBehaviour
{
    public int ItemID;
    public ItemType Type;
    public bool canBeUsed;
    public int Quantity = 1;
    public bool isPlaced;
    public Sprite itemIconSprite;
    public bool IsStackable = true;
    public string DisplayName;
    public void Trigger()
    {
        gameObject.SetActive(true);
    }

}

public enum ItemType
{
    key
}
