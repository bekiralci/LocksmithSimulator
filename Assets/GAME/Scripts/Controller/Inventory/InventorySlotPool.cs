// Slot içindeki objeleri yöneten havuz
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotPool : MonoBehaviour
{
    [SerializeField] private List<Item> objectPool = new List<Item>();

    public Item GetObjectFromPool()
    {
        if (objectPool.Count > 0)
        {
            Item obj = objectPool[0];
            objectPool.RemoveAt(0);
            return obj;
        }
        return null;
    }

    public void ReturnObjectToPool(Item obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Add(obj);
    }
}
