// Assets/NPCSystem/Managers/NPCSystemFacade.cs
using UnityEngine;

namespace NPCSystem
{
    public class NPCSystemFacade : MonoBehaviour
    {
        [Tooltip("SpawnManager referansýný Inspector'dan ata")]
        public SpawnManager spawnManager;

        public void SpawnRandomNPC() => spawnManager.SpawnRandom();
        public void ResetAllNPCs() => EventBus.ResetNPCs();
    }
}
