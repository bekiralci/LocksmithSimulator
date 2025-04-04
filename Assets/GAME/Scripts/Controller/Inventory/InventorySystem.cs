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
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Yardýmcý sýnýflarýn örneklerini oluþtur
        slotManager = gameObject.AddComponent<SlotManager>();
        slotManager.Initialize(slotPrefab, hotbarPanel, slotCount, highlightIcon);
        itemManager = new ItemManager();
        inputHandler = gameObject.AddComponent<InputHandler>();
        inputHandler.Initialize(slotManager, itemManager);
    }

    void Start()
    {
        slotManager.GenerateSlots(); // Envanter slotlarýný oluþtur
        slotManager.UpdateSlotSelection(); // Seçili slotu vurgula
    }

    void Update()
    {
        inputHandler.HandleUserInput(); // Kullanýcý giriþlerini yönet
    }

    public void AddItem(Item newItem, int amount)
    {
        itemManager.AddItemToInventory(newItem, amount, slotManager);
    }

    // Seçili slotu dýþarýdan eriþilebilen þekilde döndüren fonksiyon
    public InventorySlot GetSelectedSlot()
    {
        return slotManager.GetSelectedSlot();
    }
}
