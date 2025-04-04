using UnityEngine;
using UnityEngine.UI;
// E�ya s�n�f�
public class Item : MonoBehaviour
{
    public string itemID;
    public ItemType itemType;
    public bool canBeUsed;
    public Sprite itemIconSprite;
}

public enum ItemType
{
    Key,
    Keychain,
    CarRemoteControl
}
