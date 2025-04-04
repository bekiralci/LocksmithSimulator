using UnityEngine;
using UnityEngine.UI;
// Eþya sýnýfý
public class Item : MonoBehaviour
{
    public int itemID;
    public ItemType itemType;
    public bool canBeUsed;
    public bool isPlaced;
    public Sprite itemIconSprite;

    public void Trigger()
    {
        gameObject.SetActive(true);
    }
}

public enum ItemType
{
    Key,
    Keychain,
    CarRemoteControl
}
