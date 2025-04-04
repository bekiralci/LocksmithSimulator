using UnityEngine;

public class Key : Item, ILeftClickListener
{
    public bool isOriginal;
    public void OnLeftClick()
    {
        if (isPlaced)
        {
            return;
        }
        InventorySystem.Instance.AddItem(this, 1);
        gameObject.SetActive(false);
    }
}
