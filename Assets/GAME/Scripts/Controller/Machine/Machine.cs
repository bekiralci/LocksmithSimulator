using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour, ILeftClickListener, IRightClickListener
{
    [Header("Makine Ayarlar�")]
    [SerializeField] private Transform originalObjectTransform; // Orijinal obje konumu
    [SerializeField] private Transform newObjectTransform; // Yeni obje konumu
    [SerializeField] private float operationTime = 3.0f; // Makinenin �al��ma s�resi

    [SerializeField] private Item originalObject;
    [SerializeField] private Item newObject;
    private bool isOperating = false; // Makine �al���yor mu kontrol�
    private bool machineWorked = false; // Makine �al���yor mu kontrol�

    // Ses efektleri (�u anda yorum sat�r�na al�nd�)
    // [Header("Ses Efektleri")]
    // [SerializeField] private AudioClip keyAddedSound; // Anahtar eklendi sesi
    // [SerializeField] private AudioClip keyRemovedSound; // Anahtar ��kar�ld� sesi
    // [SerializeField] private AudioClip machineRunningSound; // Makine �al���yor sesi
    // [SerializeField] private AudioClip operationFinishedSound; // Makine biti� sesi

    private AudioSource audioSource; // AudioSource bile�eni

    void Start()
    {
        // audioSource = GetComponent<AudioSource>(); // AudioSource bile�enini al
    }

    public bool AddObject()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine �al���rken obje eklenemez!");
            return false;
        }

        Item obj = InventorySystem.Instance.GetSelectedSlot().GetItemForInfo();

        if (obj == null || obj is not Key key)
        {
            Debug.Log("Obj null ya da key de�il.");
            return false;
        }

        if (key.isOriginal)
        {
            return TrySetObject(ref originalObject, originalObjectTransform, "[Makine] Orijinal obje ba�ar�yla eklendi.");
        }
        else
        {
            if (originalObject == null)
            {
                Debug.Log("Orijinal obje eklenmeden yeni obje eklenemez.");
                return false;
            }
            return TrySetObject(ref newObject, newObjectTransform, "[Makine] Yeni obje ba�ar�yla eklendi.");
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
            Debug.LogWarning("[Makine] Makine �al���rken obje ��kar�lamaz!");
            return false;
        }

        if (newObject != null)
            return RemoveSingleObject(ref newObject, "Yeni obje ��kar�ld�.");

        if (originalObject != null)
            return RemoveSingleObject(ref originalObject, "Orijinal obje ��kar�ld�.");

        Debug.LogWarning("[Makine] Makinede ��kar�lacak obje bulunmuyor!");
        return false;
    }

    private bool RemoveBothObjects()
    {
        AddItemToInventory(originalObject);
        AddItemToInventory(newObject);
        ResetMachine();
        Debug.Log("[Makine] T�m objeler ��kar�ld�.");
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

    // Sol t�klama i�lemi (objeyi ekleme)
    public void OnLeftClick()
    {
        AddObject();
    }

    // Sa� t�klama i�lemi (objeyi ��karma)
    public void OnRightClick()
    {
        RemoveObject();
    }

    // Objeyi belirli bir transform noktas�na yerle�tir
    private void PlaceObject(Item obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }

    // Makineyi �al��t�r (Coroutine ile gecikmeli i�lem)
    public void OperateMachine()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine zaten �al���yor, l�tfen bekleyin!");
            return;
        }

        if (originalObject == null || newObject == null)
        {
            Debug.LogWarning("[Makine] Makinenin �al��mas� i�in hem orijinal hem de yeni obje eklenmelidir!");
            return;
        }

        // PlaySound(machineRunningSound); // Makine �al���rken ses �al
        StartCoroutine(OperateMachineCoroutine());
    }

    private IEnumerator OperateMachineCoroutine()
    {
        isOperating = true;
        Debug.Log("[Makine] Makine �al��maya ba�lad�. S�re: " + operationTime + " saniye");
        yield return new WaitForSeconds(operationTime);

        Destroy(newObject.gameObject);
        newObject = Instantiate(originalObject, newObjectTransform.position, newObjectTransform.rotation);
        // PlaySound(operationFinishedSound); // Makine tamamland���nda ses �al
        Debug.Log("[Makine] Makine �al��t�r�ld�: Yeni obje, orijinal objenin klonuyla de�i�tirildi.");

        machineWorked = true;
        isOperating = false;
    }

    // Makineyi s�f�rla
    public void ResetMachine()
    {
        originalObject = null;
        newObject = null;
        isOperating = false;
        machineWorked = false;
    }
}

