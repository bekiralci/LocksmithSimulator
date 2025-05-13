// Assets/NPCSystem/EventBus.cs
using System;
using UnityEngine;

namespace NPCSystem
{
    public static class EventBus
    {
        public static event Action OnResetNPCs;

        public static void ResetNPCs()
        {
            try
            {
                OnResetNPCs?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Hata: NPC resetlenirken exception oluştu - {ex}");
            }
        }
    }
}
