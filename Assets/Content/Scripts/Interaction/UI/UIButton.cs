using System;
using UnityEngine;

namespace Interaction.UI
{
    public class UIButton : UIControlBase
    {
        public EventHandler OnButtonClicked;

        [SerializeField] private UIColorTransition transition;
        [SerializeField] private UIColorBlock colorBlock;

        protected override void OnControlSelected()
        {
            OnButtonClicked?.Invoke(this, null);
        }

        protected override void OnStateChanged(UIControlState state)
        {
            transition.TransitionTo(colorBlock.GetColorFromState(state));
        }

        void Update()
        {
            transition.Tick();
        }
    }
}