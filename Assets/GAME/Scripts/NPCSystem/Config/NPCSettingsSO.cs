// Assets/NPCSystem/Config/NPCSettingsSO.cs
using UnityEngine;

namespace NPCSystem
{
    public class NPCSettingsSO : MonoBehaviour
    {
        [Header("Spawn Noktalarý")]
        [Tooltip("NPC'lerin doðacaðý Transform noktalarý")]
        public Transform[] spawnPoints;

        [Header("Hedef Bölge")]
        [Tooltip("NPC'lerin gitmesi gereken BoxCollider (isTrigger = true)")]
        public BoxCollider targetArea;

        [Header("Object Pooling")]
        [Tooltip("Havuzdaki NPC adedi")]
        public int poolSize = 20;
    }
}
