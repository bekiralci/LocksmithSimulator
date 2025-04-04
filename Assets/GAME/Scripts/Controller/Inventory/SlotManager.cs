using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    private GameObject slotPrefab;
    private Transform hotbarPanel;
    public Dictionary<int, InventorySlot> SlotDictionary = new Dictionary<int, InventorySlot>();
    private int selectedSlotIndex = 0;
    private Image highlightIcon;

    public int SlotCount { get; private set; }

    public int SelectedSlotIndex => selectedSlotIndex;

    // Constructor kaldýrýldý, parametreler Initialize ile alýnacak.
    public void Initialize(GameObject slotPrefab, Transform hotbarPanel, int slotCount, Image highlightIcon)
    {
        this.slotPrefab = slotPrefab;
        this.hotbarPanel = hotbarPanel;
        this.SlotCount = slotCount;
        this.highlightIcon = highlightIcon;
    }

    public void GenerateSlots()
    {
        float slotWidth = slotPrefab.GetComponent<RectTransform>().rect.width;
        float slotSpacing = 10f;

        for (int i = 0; i < SlotCount; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, hotbarPanel);
            InventorySlot slotComponent = newSlot.GetComponent<InventorySlot>();
            slotComponent.InitializeSlot(i);

            RectTransform slotRectTransform = newSlot.GetComponent<RectTransform>();
            slotRectTransform.anchoredPosition = new Vector2(i * (slotWidth + slotSpacing), 0);

            SlotDictionary.Add(i, slotComponent);
        }
    }

    public void UpdateSlotSelection()
    {
        RectTransform selectedSlotRect = SlotDictionary[selectedSlotIndex].GetComponent<RectTransform>();
        highlightIcon.rectTransform.anchoredPosition = selectedSlotRect.anchoredPosition;
    }

    public void SelectSlot(int index)
    {
        selectedSlotIndex = index;
        UpdateSlotSelection();
    }

    public void SelectNextSlot()
    {
        selectedSlotIndex = (selectedSlotIndex + 1) % SlotCount;
        UpdateSlotSelection();
    }

    public void SelectPreviousSlot()
    {
        selectedSlotIndex = (selectedSlotIndex - 1 + SlotCount) % SlotCount;
        UpdateSlotSelection();
    }

    public bool HasItemInSelectedSlot()
    {
        return SlotDictionary[selectedSlotIndex].HasItem();
    }

    public InventorySlot GetSelectedSlot()
    {
        return SlotDictionary[selectedSlotIndex];
    }

    public Item GetItemInSelectedSlot()
    {
        return SlotDictionary[selectedSlotIndex].GetItemForInfo();
    }
}
