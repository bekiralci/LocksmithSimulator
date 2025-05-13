// Assets/NPCSystem/States/StateManager.cs
namespace NPCSystem
{
    public class StateManager
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            if (_currentState == newState) return;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Update() => _currentState?.Execute();
    }
}
