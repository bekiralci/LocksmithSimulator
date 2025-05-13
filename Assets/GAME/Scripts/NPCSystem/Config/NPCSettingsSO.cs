// Assets/NPCSystem/Config/NPCSettingsSO.cs
using UnityEngine;

namespace NPCSystem
{
    public class NPCSettingsSO : MonoBehaviour
    {
        [Header("Spawn Noktalar�")]
        [Tooltip("NPC'lerin do�aca�� Transform noktalar�")]
        public Transform[] spawnPoints;

        [Header("Hedef B�lge")]
        [Tooltip("NPC'lerin gitmesi gereken BoxCollider (isTrigger = true)")]
        public BoxCollider targetArea;

        [Header("Object Pooling")]
        [Tooltip("Havuzdaki NPC adedi")]
        public int poolSize = 20;
    }
}
