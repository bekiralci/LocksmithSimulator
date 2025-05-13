// Assets/NPCSystem/Managers/SpawnManager.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPCSystem
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Config")]
        public NPCSettingsSO settings;

        [Header("Scene References")]
        public Transform[] spawnPoints;
        public BoxCollider targetArea;

        [Header("Prefab")]
        public GameObject npcPrefab;

        private List<NPCController> _controllers;

        void Start()
        {
            // Havuz kurulumunu Start'ta yapıyoruz
            _controllers = new List<NPCController>(settings.poolSize);
            for (int i = 0; i < settings.poolSize; i++)
            {
                GameObject obj = Instantiate(npcPrefab);
                obj.SetActive(false);
                _controllers.Add(obj.GetComponent<NPCController>());
            }
        }

        public NPCController SpawnRandom()
        {
            var ctrl = _controllers.Find(c => !c.gameObject.activeSelf);
            if (ctrl == null)
            {
                Debug.LogWarning("SpawnManager: Havuzda boş NPC yok");
                return null;
            }

            int idx = Random.Range(0, spawnPoints.Length);
            Vector3 pos = spawnPoints[idx].position;

            ctrl.gameObject.SetActive(true);
            ctrl.transform.position = pos;
            ctrl.SpawnPosition = pos;
            ctrl.MoveToTargetArea();
            return ctrl;
        }
    }
}
