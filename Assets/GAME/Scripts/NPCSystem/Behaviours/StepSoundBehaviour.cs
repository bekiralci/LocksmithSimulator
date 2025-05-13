// Assets/NPCSystem/Behaviours/StepSoundBehaviour.cs
using UnityEngine;

namespace NPCSystem
{
    public class StepSoundBehaviour : StateMachineBehaviour
    {
        public AudioClip stepClip;
        private AudioSource _source;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_source == null)
                _source = animator.GetComponent<AudioSource>();
            if (_source != null && stepClip != null)
                _source.PlayOneShot(stepClip);
        }
    }
}
