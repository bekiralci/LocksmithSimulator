// Assets/NPCSystem/Controllers/NPCController.cs
using UnityEngine;
using UnityEngine.AI;

namespace NPCSystem
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class NPCController : MonoBehaviour
    {
        [HideInInspector] public Vector3 SpawnPosition;

        public NavMeshAgent Agent { get; private set; }
        public Animator AnimatorComponent { get; private set; }

        public IMovementStrategy MovementStrategy { get; set; }

        private StateManager _stateManager;
        private IdleState _idleState;
        private WalkState _walkState;
        private CommandInvoker _invoker;

        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            AnimatorComponent = GetComponent<Animator>();

            MovementStrategy = new RandomMovement();
        }

        void OnEnable() => EventBus.OnResetNPCs += HandleReset;
        void OnDisable() => EventBus.OnResetNPCs -= HandleReset;

        void HandleReset()
        {
            _invoker.ClearCommand();
            _invoker.SetCommand(new ReturnToSpawnCommand(this));
            _invoker.Invoke();
        }

        void Start()
        {
            _stateManager = new StateManager();
            _idleState = new IdleState(AnimatorComponent);
            _walkState = new WalkState(AnimatorComponent);
            _stateManager.ChangeState(_idleState);

            _invoker = new CommandInvoker();
        }

        void Update()
        {
            _stateManager.Update();
            float speed = Agent.velocity.magnitude;
            if (speed > 0.1f) _stateManager.ChangeState(_walkState);
            else _stateManager.ChangeState(_idleState);
        }

        /// <summary>
        /// SpawnManager tarafýndan konum atamasý yapýldýktan sonra hedef bölgeye yürü.
        /// </summary>
        public void MoveToTargetArea()
        {
            MovementStrategy.Move(Agent, FindObjectOfType<NPCSettingsSO>().targetArea);
        }

        /// <summary>
        /// Spawn pozisyonuna dön.
        /// </summary>
        public void ReturnToSpawn()
        {
            Agent.SetDestination(SpawnPosition);
        }

        void OnDrawGizmosSelected()
        {
            var settings = FindObjectOfType<NPCSettingsSO>();
            if (settings != null && settings.targetArea != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(
                    settings.targetArea.bounds.center,
                    settings.targetArea.bounds.size
                );
            }
        }

        void OnMouseDown()
        {
            // Fareyle bu NPC'nin collider'ýna týklandýðýnda spawn noktasýna dön
            ReturnToSpawn();
        }

    }
}
