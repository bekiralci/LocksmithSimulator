using UnityEngine;

public class MachineTrigger : MonoBehaviour, ILeftClickListener
{
    [Header("Tetikleyici Ayarlarý")]
    [SerializeField] private Machine machine;

    // Makineyi tetikleyen fonksiyon
    public void ActivateMachine()
    {
        if (machine == null)
        {
            Debug.LogWarning("[MakineTetikleyici] Baðlý bir makine bulunamadý!");
            return;
        }

        machine.OperateMachine();
        Debug.Log("[MakineTetikleyici] Makine tetiklendi ve çalýþtýrýldý!");
    }

    public void OnLeftClick()
    {
        ActivateMachine();
    }
}
