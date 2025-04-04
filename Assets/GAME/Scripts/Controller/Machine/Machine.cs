using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour, ILeftClickListener, IRightClickListener
{
    [Header("Makine Ayarlarý")]
    [SerializeField] private Transform originalObjectTransform; // Orijinal obje konumu
    [SerializeField] private Transform newObjectTransform; // Yeni obje konumu
    [SerializeField] private float operationTime = 3.0f; // Makinenin çalýþma süresi

    [SerializeField] private Item originalObject;
    [SerializeField] private Item newObject;
    private bool isOperating = false; // Makine çalýþýyor mu kontrolü
    private bool machineWorked = false; // Makine çalýþýyor mu kontrolü

    // Ses efektleri (þu anda yorum satýrýna alýndý)
    // [Header("Ses Efektleri")]
    // [SerializeField] private AudioClip keyAddedSound; // Anahtar eklendi sesi
    // [SerializeField] private AudioClip keyRemovedSound; // Anahtar çýkarýldý sesi
    // [SerializeField] private AudioClip machineRunningSound; // Makine çalýþýyor sesi
    // [SerializeField] private AudioClip operationFinishedSound; // Makine bitiþ sesi

    private AudioSource audioSource; // AudioSource bileþeni

    void Start()
    {
        // audioSource = GetComponent<AudioSource>(); // AudioSource bileþenini al
    }

    public bool AddObject()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine çalýþýrken obje eklenemez!");
            return false;
        }

        Item obj = InventorySystem.Instance.GetSelectedSlot().GetItemForInfo();

        if (obj == null || obj is not Key key)
        {
            Debug.Log("Obj null ya da key deðil.");
            return false;
        }

        if (key.isOriginal)
        {
            return TrySetObject(ref originalObject, originalObjectTransform, "[Makine] Orijinal obje baþarýyla eklendi.");
        }
        else
        {
            if (originalObject == null)
            {
                Debug.Log("Orijinal obje eklenmeden yeni obje eklenemez.");
                return false;
            }
            return TrySetObject(ref newObject, newObjectTransform, "[Makine] Yeni obje baþarýyla eklendi.");
        }
    }

    private bool TrySetObject(ref Item targetSlot, Transform targetTransform, string successMessage)
    {
        if (targetSlot != null) return false;

        Item obj = InventorySystem.Instance.GetSelectedSlot().GetItemForUse();
        targetSlot = obj;
        PlaceObject(obj, targetTransform);
        obj.gameObject.SetActive(true);
        Debug.Log(successMessage);
        return true;
    }

    public bool RemoveObject()
    {
        if (machineWorked)
            return RemoveBothObjects();

        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine çalýþýrken obje çýkarýlamaz!");
            return false;
        }

        if (newObject != null)
            return RemoveSingleObject(ref newObject, "Yeni obje çýkarýldý.");

        if (originalObject != null)
            return RemoveSingleObject(ref originalObject, "Orijinal obje çýkarýldý.");

        Debug.LogWarning("[Makine] Makinede çýkarýlacak obje bulunmuyor!");
        return false;
    }

    private bool RemoveBothObjects()
    {
        AddItemToInventory(originalObject);
        AddItemToInventory(newObject);
        ResetMachine();
        Debug.Log("[Makine] Tüm objeler çýkarýldý.");
        return true;
    }

    private bool RemoveSingleObject(ref Item item, string message)
    {
        AddItemToInventory(item);
        item = null;
        Debug.Log($"[Makine] {message}");
        return true;
    }

    private void AddItemToInventory(Item item)
    {
        if (item != null)
        {
            InventorySystem.Instance.AddItem(item, 1);
        }
    }

    // Sol týklama iþlemi (objeyi ekleme)
    public void OnLeftClick()
    {
        AddObject();
    }

    // Sað týklama iþlemi (objeyi çýkarma)
    public void OnRightClick()
    {
        RemoveObject();
    }

    // Objeyi belirli bir transform noktasýna yerleþtir
    private void PlaceObject(Item obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }

    // Makineyi çalýþtýr (Coroutine ile gecikmeli iþlem)
    public void OperateMachine()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine zaten çalýþýyor, lütfen bekleyin!");
            return;
        }

        if (originalObject == null || newObject == null)
        {
            Debug.LogWarning("[Makine] Makinenin çalýþmasý için hem orijinal hem de yeni obje eklenmelidir!");
            return;
        }

        // PlaySound(machineRunningSound); // Makine çalýþýrken ses çal
        StartCoroutine(OperateMachineCoroutine());
    }

    private IEnumerator OperateMachineCoroutine()
    {
        isOperating = true;
        Debug.Log("[Makine] Makine çalýþmaya baþladý. Süre: " + operationTime + " saniye");
        yield return new WaitForSeconds(operationTime);

        Destroy(newObject.gameObject);
        newObject = Instantiate(originalObject, newObjectTransform.position, newObjectTransform.rotation);
        // PlaySound(operationFinishedSound); // Makine tamamlandýðýnda ses çal
        Debug.Log("[Makine] Makine çalýþtýrýldý: Yeni obje, orijinal objenin klonuyla deðiþtirildi.");

        machineWorked = true;
        isOperating = false;
    }

    // Makineyi sýfýrla
    public void ResetMachine()
    {
        originalObject = null;
        newObject = null;
        isOperating = false;
        machineWorked = false;
    }
}

