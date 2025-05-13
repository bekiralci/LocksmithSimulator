// Assets/NPCSystem/Strategies/PatrolMovement.cs
using UnityEngine;
using UnityEngine.AI;

namespace NPCSystem
{
    public class PatrolMovement : IMovementStrategy
    {
        private Vector3[] _points;
        private int _idx;

        public PatrolMovement(Vector3[] points)
        {
            _points = points ?? new Vector3[0];
        }

        public void Move(NavMeshAgent agent, BoxCollider area)
        {
            if (_points.Length == 0)
            {
                Debug.LogWarning("PatrolMovement: rota noktası yok");
                return;
            }

            agent.SetDestination(_points[_idx]);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                _idx = (_idx + 1) % _points.Length;
        }
    }
}
