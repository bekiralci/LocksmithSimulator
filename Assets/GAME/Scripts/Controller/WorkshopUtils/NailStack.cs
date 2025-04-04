using System.Collections.Generic;
using UnityEngine;

public class NailStack : MonoBehaviour, IRightClickListener, ILeftClickListener
{
    private Stack<Item> objectStack = new(); // Stack veri yap�s�
    private int itemID = 0;
    private const int maxStackSize = 5; // Maksimum stack boyutu

    public void OnLeftClick()
    {
        Item selectedItem = InventorySystem.Instance.GetSelectedSlot()?.GetItemForInfo();
        if (selectedItem is not Key key || key.isOriginal)
        {
            Debug.Log("Se�ilen obje ge�ersiz veya orijinal anahtar.");
            return;
        }

        Item itemToUse = InventorySystem.Instance.GetSelectedSlot()?.GetItemForUse();
        if ((key.itemID == itemID || objectStack.Count == 0) && objectStack.Count < maxStackSize)
        {
            AddObjectToNail(itemToUse);
        }
        else
        {
            Debug.Log("Stack maksimum kapasiteye ula�t�.");
        }
    }

    public void OnRightClick()
    {
        RemoveObjectFromNail();
    }

    private void AddObjectToNail(Item item)
    {
        if (item == null || objectStack.Count >= maxStackSize) return;

        if (objectStack.Count == 0)
        {
            itemID = item.itemID;
        }

        objectStack.Push(item);
        item.transform.SetParent(transform);
        item.transform.position = GetStackedItemPosition();
        item.Trigger();
        item.isPlaced = true;
    }

    private void RemoveObjectFromNail()
    {
        if (objectStack.Count > 0)
        {
            Item removedItem = objectStack.Pop();
            AddItemToInventory(removedItem);
            removedItem.isPlaced = false;
            if (objectStack.Count == 0)
            {
                itemID = 0;
            }
        }
    }

    private void AddItemToInventory(Item item)
    {
        if (item != null)
        {
            InventorySystem.Instance.AddItem(item, 1);
        }
    }

    private Vector3 GetStackedItemPosition()
    {
        return transform.position + new Vector3(objectStack.Count * 0.3f, 0, 0);
    }

    public void ShowStack()
    {
        Debug.Log($"Stack'teki obje say�s�: {objectStack.Count}");
    }
}
