// Assets/NPCSystem/Strategies/RandomMovement.cs
using UnityEngine;
using UnityEngine.AI;

namespace NPCSystem
{
    public class RandomMovement : IMovementStrategy
    {
        public void Move(NavMeshAgent agent, BoxCollider area)
        {
            if (area == null)
            {
                Debug.LogError("RandomMovement: targetArea null");
                return;
            }

            var bounds = area.bounds;
            Vector3 target = bounds.center + new Vector3(
                Random.Range(-bounds.extents.x, bounds.extents.x),
                0,
                Random.Range(-bounds.extents.z, bounds.extents.z)
            );
            agent.SetDestination(target);
        }
    }
}
