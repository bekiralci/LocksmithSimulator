// Assets/NPCSystem/States/IdleState.cs
using UnityEngine;

namespace NPCSystem
{
    public class IdleState : IState
    {
        private readonly Animator _anim;

        public IdleState(Animator anim) => _anim = anim;

        public void Enter() => _anim.SetBool("isWalking", false);
        public void Execute() { }
        public void Exit() { }
    }
}
