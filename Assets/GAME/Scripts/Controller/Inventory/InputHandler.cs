using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private SlotManager slotManager;
    private ItemManager itemManager;

    // Initialize methodu, SlotManager ve ItemManager'ý parametre olarak alacak
    public void Initialize(SlotManager slotManager, ItemManager itemManager)
    {
        this.slotManager = slotManager;
        this.itemManager = itemManager;
    }

    public void HandleUserInput()
    {
        HandleScrollInput();
        HandleHotkeyInput();
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            slotManager.SelectNextSlot();
        }
        else if (scroll < 0)
        {
            slotManager.SelectPreviousSlot();
        }
    }

    private void HandleHotkeyInput()
    {
        for (int i = 0; i < slotManager.SlotCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                slotManager.SelectSlot(i);
            }
        }
    }
    
}
