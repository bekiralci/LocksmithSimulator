using UnityEngine;

public class MachineTrigger : MonoBehaviour, ILeftClickListener
{
    [Header("Tetikleyici Ayarlar�")]
    [SerializeField] private Machine machine;

    // Makineyi tetikleyen fonksiyon
    public void ActivateMachine()
    {
        if (machine == null)
        {
            Debug.LogWarning("[MakineTetikleyici] Ba�l� bir makine bulunamad�!");
            return;
        }

        machine.OperateMachine();
        Debug.Log("[MakineTetikleyici] Makine tetiklendi ve �al��t�r�ld�!");
    }

    public void OnLeftClick()
    {
        ActivateMachine();
    }
}
