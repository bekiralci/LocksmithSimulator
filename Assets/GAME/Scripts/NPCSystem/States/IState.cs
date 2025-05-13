// Assets/NPCSystem/States/IState.cs
namespace NPCSystem
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
