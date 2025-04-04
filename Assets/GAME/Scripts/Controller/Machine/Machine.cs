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

        if (obj == null)
        {
            print("obj is null");
            return false;
        }
        if (obj is Key key)
        {

            if (key.isOriginal)
            {
                if (originalObject == null)
                {
                    obj = InventorySystem.Instance.GetSelectedSlot().GetItemForUse();
                    originalObject = obj;
                    PlaceObject(obj, originalObjectTransform);
                    obj.gameObject.SetActive(true);
                    Debug.Log("[Makine] Orijinal obje baþarýyla eklendi.");
                    return true;
                }
            }
            else if (!key.isOriginal)
            {
                print("new object ");
                if (originalObject == null)
                {
                    print("original object is null");
                    return false;
                }
                obj = InventorySystem.Instance.GetSelectedSlot().GetItemForUse();
                newObject = obj;
                PlaceObject(obj, newObjectTransform);
                newObject.gameObject.SetActive(true);
                Debug.Log("[Makine] Yeni obje baþarýyla eklendi.");
                return true;
            }

        }

        // Eðer her iki obje de doluysa ekleme yapýlamaz
        Debug.LogWarning("[Makine] Makine zaten dolu! Daha fazla obje eklenemez.");
        return false;
    }
    public bool RemoveObject()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine çalýþýrken obje çýkarýlamaz!");
            return false;
        }

        if (newObject != null)
        {
            Destroy(newObject);
            newObject = null;
            Debug.Log("[Makine] Yeni obje çýkarýldý.");
            return true;
        }

        if (originalObject != null)
        {
            Destroy(originalObject);
            originalObject = null;
            Debug.Log("[Makine] Orijinal obje çýkarýldý.");
            return true;
        }

        Debug.LogWarning("[Makine] Makinede çýkarýlacak obje bulunmuyor!");
        return false;
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

        Destroy(newObject);
        newObject = Instantiate(originalObject, newObjectTransform.position, newObjectTransform.rotation);
        // PlaySound(operationFinishedSound); // Makine tamamlandýðýnda ses çal
        Debug.Log("[Makine] Makine çalýþtýrýldý: Yeni obje, orijinal objenin klonuyla deðiþtirildi.");

        isOperating = false;
    }

    // Makineyi sýfýrla
    public void ResetMachine()
    {
        originalObject = null;
        newObject = null;
        isOperating = false;
        Debug.Log("[Makine] Makine sýfýrlandý.");
    }
}

