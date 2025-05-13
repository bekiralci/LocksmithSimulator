// Assets/NPCSystem/TestRuntime/NPCTestRunner.cs
using System.Collections;
using UnityEngine;

namespace NPCSystem
{
    public class NPCTestRunner : MonoBehaviour
    {
        public NPCSystemFacade facade;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(3);            // bir frame bekle
            facade.SpawnRandomNPC();      // �imdi pool haz�r
            facade.SpawnRandomNPC();      // �imdi pool haz�r
            facade.SpawnRandomNPC();      // �imdi pool haz�r
            facade.SpawnRandomNPC();      // �imdi pool haz�r
        }
    }
}
