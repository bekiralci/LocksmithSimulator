using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform hotbarPanel;
    [SerializeField] private int slotCount = 5;
    [SerializeField] private Image highlightIcon;

    private SlotManager slotManager;
    private ItemManager itemManager;
    private InputHandler inputHandler;

    // Singleton instance
    public static InventorySystem Instance { get; private set; }

    void Awake()
    {
        // Singleton kontrol�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Yard�mc� s�n�flar�n �rneklerini olu�tur
        slotManager = gameObject.AddComponent<SlotManager>();
        slotManager.Initialize(slotPrefab, hotbarPanel, slotCount, highlightIcon);
        itemManager = new ItemManager();
        inputHandler = gameObject.AddComponent<InputHandler>();
        inputHandler.Initialize(slotManager, itemManager);
    }

    void Start()
    {
        slotManager.GenerateSlots(); // Envanter slotlar�n� olu�tur
        slotManager.UpdateSlotSelection(); // Se�ili slotu vurgula
    }

    void Update()
    {
        inputHandler.HandleUserInput(); // Kullan�c� giri�lerini y�net
    }

    public void AddItem(Item newItem, int amount)
    {
        itemManager.AddItemToInventory(newItem, amount, slotManager);
    }

    // Se�ili slotu d��ar�dan eri�ilebilen �ekilde d�nd�ren fonksiyon
    public InventorySlot GetSelectedSlot()
    {
        return slotManager.GetSelectedSlot();
    }
}
