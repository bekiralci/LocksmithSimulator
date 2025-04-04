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
                    Debug.Log("[Makine] Orijinal obje ba�ar�yla eklendi.");
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
                Debug.Log("[Makine] Yeni obje ba�ar�yla eklendi.");
                return true;
            }

        }

        // E�er her iki obje de doluysa ekleme yap�lamaz
        Debug.LogWarning("[Makine] Makine zaten dolu! Daha fazla obje eklenemez.");
        return false;
    }
    public bool RemoveObject()
    {
        if (isOperating)
        {
            Debug.LogWarning("[Makine] Makine �al���rken obje ��kar�lamaz!");
            return false;
        }

        if (newObject != null)
        {
            Destroy(newObject);
            newObject = null;
            Debug.Log("[Makine] Yeni obje ��kar�ld�.");
            return true;
        }

        if (originalObject != null)
        {
            Destroy(originalObject);
            originalObject = null;
            Debug.Log("[Makine] Orijinal obje ��kar�ld�.");
            return true;
        }

        Debug.LogWarning("[Makine] Makinede ��kar�lacak obje bulunmuyor!");
        return false;
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

        Destroy(newObject);
        newObject = Instantiate(originalObject, newObjectTransform.position, newObjectTransform.rotation);
        // PlaySound(operationFinishedSound); // Makine tamamland���nda ses �al
        Debug.Log("[Makine] Makine �al��t�r�ld�: Yeni obje, orijinal objenin klonuyla de�i�tirildi.");

        isOperating = false;
    }

    // Makineyi s�f�rla
    public void ResetMachine()
    {
        originalObject = null;
        newObject = null;
        isOperating = false;
        Debug.Log("[Makine] Makine s�f�rland�.");
    }
}

