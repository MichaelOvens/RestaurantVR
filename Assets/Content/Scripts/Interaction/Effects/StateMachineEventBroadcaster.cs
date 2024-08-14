using UnityEngine;

namespace Interaction.Effects
{
    public interface IOnStateMachineStateEnter { public void OnStateMachineStateEnter(string label); }
    public interface IOnStateMachineStateExit { public void OnStateMachineStateExit(string label); }

    public class StateMachineEventBroadcaster : StateMachineBehaviour
    {
        [SerializeField] private string _label;
        [SerializeField] private bool _onStateEnter;
        [SerializeField] private bool _onStateExit;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_onStateEnter) return;

            var receivers = animator.GetComponents<IOnStateMachineStateEnter>();
            foreach (var receiver in receivers)
            {
                receiver.OnStateMachineStateEnter(_label);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_onStateExit) return;

            var receivers = animator.GetComponents<IOnStateMachineStateExit>();
            foreach (var receiver in receivers)
            {
                receiver.OnStateMachineStateExit(_label);
            }
        }
    }
}
