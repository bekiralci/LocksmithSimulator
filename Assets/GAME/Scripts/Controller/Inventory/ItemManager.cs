public class ItemManager
{
    public void AddItemToInventory(Item newItem, int amount, SlotManager slotManager)
    {

        newItem.gameObject.SetActive(false); // Yeni eþyayý gizle
        foreach (var slot in slotManager.SlotDictionary.Values)
        {
            if (slot.CanStackItem(newItem))
            {
                amount = slot.StackItem(newItem, amount);
                if (amount <= 0) return;
            }
        }
        foreach (var slot in slotManager.SlotDictionary.Values)
        {
            if (slot.IsEmpty())
            {
                slot.AssignItem(newItem, amount);
                return;
            }
        }
    }
}
