// Assets/NPCSystem/States/WalkState.cs
using UnityEngine;

namespace NPCSystem
{
    public class WalkState : IState
    {
        private readonly Animator _anim;

        public WalkState(Animator anim) => _anim = anim;

        public void Enter() => _anim.SetBool("isWalking", true);
        public void Execute() { }
        public void Exit() => _anim.SetBool("isWalking", false);
    }
}
