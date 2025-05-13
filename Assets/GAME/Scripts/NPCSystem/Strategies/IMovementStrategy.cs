// Assets/NPCSystem/Strategies/IMovementStrategy.cs
using UnityEngine;
using UnityEngine.AI;

namespace NPCSystem
{
    public interface IMovementStrategy
    {
        void Move(NavMeshAgent agent, BoxCollider area);
    }
}
