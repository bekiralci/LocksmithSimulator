using UnityEngine;

public class Key : Item, ILeftClickListener
{
    public bool isOriginal;

    public void OnLeftClick()
    {
        InventorySystem.Instance.AddItem(this, 1);
        gameObject.SetActive(false);
    }
}
