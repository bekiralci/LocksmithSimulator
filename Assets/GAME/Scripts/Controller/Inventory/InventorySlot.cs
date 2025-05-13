using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{

    [Tooltip("Slotun havuzu")]
    public InventorySlotPool slotPool;

    [Tooltip("Slotun indeksi")]
    public int slotIndex;

    [Tooltip("Slotta bulunan eþya")]
    public Item item;

    [Tooltip("Eþya miktarý")]
    public int itemCount;

    [Tooltip("Eþya miktarýný gösteren metin")]
    public TextMeshProUGUI itemCountText;

    [Tooltip("Eþya ikonu")]
    public Image itemIcon;

    [Tooltip("Maksimum stack boyutu")]
    [SerializeField] private int maxStackSize = 10;

    public void InitializeSlot(int index)
    {
        slotIndex = index;
        ClearSlot(); // Slotu baþlatýrken boþ hale getir
    }

    public bool HasItem() => slotPool.Count() > 0;
    public bool IsEmpty() => item == null;

    public bool CanStackItem(Item newItem)
    {
        return HasItem() && item.ItemID == newItem.ItemID && item.Type == newItem.Type && itemCount < maxStackSize;
    }

    public int StackItem(Item handleItem, int amount)
    {
        int spaceLeft = maxStackSize - itemCount;
        int toAdd = Mathf.Min(amount, spaceLeft);
        itemCount += toAdd;
        amount -= toAdd;
        UpdateSlot();
        slotPool.ReturnObjectToPool(handleItem);
        return amount;
    }

    public void AssignItem(Item newItem, int amount)
    {
        item = newItem;
        itemIcon.sprite = newItem.itemIconSprite;
        itemCount = Mathf.Min(amount, maxStackSize);
        slotPool.ReturnObjectToPool(newItem);
        UpdateSlot();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        UpdateSlot();
    }

    public Item GetItemForInfo()
    {
        return item;
    }

    public Item GetItemForUse()
    {
        if (HasItem() && item.canBeUsed)
        {
            Item item = slotPool.GetObjectFromPool();
            itemCount--;
            UpdateSlot();
            return item;
        }
        return null;
    }

    public void UpdateSlot()
    {
        if (HasItem()) // Eðer slotta eþya varsa
        {


            // Eþya ikonunu ve miktarýný güncelle
            if (itemIcon != null)
            {
                itemIcon.gameObject.SetActive(true);  // Ýkonu görünür yap
            }
            else
            {
                Debug.LogWarning("Item icon is not assigned!");
            }

            if (itemCountText != null)
            {
                itemCountText.text = itemCount.ToString();
                itemCountText.gameObject.SetActive(itemCount > 1);  // Sadece birden fazla eþyada miktar göster
            }
            else
            {
                Debug.LogWarning("Item count text is not assigned!");
            }
        }
        else // Eðer slot boþsa
        {
            if (itemIcon != null)
            {
                itemIcon.gameObject.SetActive(false);
            }

            if (itemCountText != null)
            {
                itemIcon.gameObject.SetActive(false);
            }
        }
    }

}

