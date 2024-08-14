using System;
using System.Diagnostics;

namespace Interaction.UI
{
    public abstract class UIControlBase : Interactable
    {
        public UIControlState State { get; private set; }

        protected abstract void OnControlSelected();
        protected abstract void OnStateChanged(UIControlState state);

        protected virtual void Awake()
        {
            OnInteractableStart += OnInteractableStateChanged;
            OnInteractableStop += OnInteractableStateChanged;
            OnHoverStart += OnInteractableStateChanged;
            OnHoverStop += OnInteractableStateChanged;
            OnPressStart += OnInteractableStateChanged;
            OnPressStop += OnInteractableStateChanged;
            OnSelected += OnInteractableStateChanged;

            OnSelected += OnInteractableSelected;
        }

        private void OnInteractableStateChanged(object sender, EventArgs e)
        {
            var newState = GetNewState();
            if (newState != State)
            {
                State = newState;
                OnStateChanged(newState);
            }
        }

        private void OnInteractableSelected(object sender, EventArgs e)
        {
            OnControlSelected();
        }

        private void UpdateState()
        {
            var newState = GetNewState();
            if (newState != State)
            {
                State = newState;
                OnStateChanged(newState);
            }
        }

        private UIControlState GetNewState()
        {
            if (!IsInteractable)
            {
                return UIControlState.Disabled;
            }
            else if (IsPressed)
            {
                return UIControlState.Pressed;
            }
            else if (IsHovered)
            {
                return UIControlState.Hovered;
            }
            else
            {
                return UIControlState.Default;
            }
        }
    }
}