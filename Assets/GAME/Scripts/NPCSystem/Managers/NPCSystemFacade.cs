// Assets/NPCSystem/Managers/NPCSystemFacade.cs
using UnityEngine;

namespace NPCSystem
{
    public class NPCSystemFacade : MonoBehaviour
    {
        [Tooltip("SpawnManager referans�n� Inspector'dan ata")]
        public SpawnManager spawnManager;

        public void SpawnRandomNPC() => spawnManager.SpawnRandom();
        public void ResetAllNPCs() => EventBus.ResetNPCs();
    }
}
