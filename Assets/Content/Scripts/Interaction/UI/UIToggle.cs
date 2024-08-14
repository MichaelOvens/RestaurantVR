using System;
using UnityEngine;

namespace Interaction.UI
{
    public class UIToggle : UIControlBase
    {
        public EventHandler<bool> OnValueChanged;

        public UIToggleGroup ToggleGroupMember
        {
            get { return _toggleGroupMember; }
            set { SetToggleGroupMember(value); }
        }
        
        public UIToggleGroup ToggleGroupExclusive
        {
            get { return _toggleGroupExclusive; }
            set { SetToggleGroupExclusive(value); }
        }

        public bool IsOn
        {
            get { return _isOn; }
            set { SetIsOn(value); }
        }

        [SerializeField] private UIToggleGroup _toggleGroupMember;
        [SerializeField] private UIToggleGroup _toggleGroupExclusive;
        [SerializeField] private UIColorTransition _transition;
        [SerializeField] private UIColorBlock _offColors;
        [SerializeField] private UIColorBlock _onColors;

        private bool _isOn = false;
        private UIControlState _state = UIControlState.Default;

        protected override void Awake()
        {
            base.Awake();
            SetToggleGroupMember(_toggleGroupMember);
        }

        private void OnDestroy()
        {
            SetToggleGroupMember(null);
        }

        protected override void OnControlSelected()
        {
            if (!IsInteractable) return;

            IsOn = !IsOn;

            // Don't go to hover after click, go instead to default
            // This makes it more obvious that the toggle has been selected
            UpdateState(UIControlState.Default);
        }

        protected override void OnStateChanged(UIControlState state)
        {
            UpdateState(state);
        }

        private bool GetIsOn()
        {
            return _isOn;
        }

        private void SetIsOn(bool value)
        {
            if (_isOn == value) return;

            _isOn = value;
            UpdateState(_state);
            OnValueChanged?.Invoke(this, IsOn);
        }

        private void SetToggleGroupMember(UIToggleGroup toggleGroup)
        {
            _toggleGroupMember?.DeregisterMember(this);
            _toggleGroupMember = toggleGroup;
            _toggleGroupMember?.RegisterMember(this);
        }

        private void SetToggleGroupExclusive(UIToggleGroup toggleGroup)
        {
            _toggleGroupExclusive?.DeregisterExclusive(this);
            _toggleGroupExclusive = toggleGroup;
            _toggleGroupExclusive?.RegisterExclusive(this);
        }

        private void UpdateState(UIControlState state)
        {
            _state = state;

            UIColorBlock colorBlock = IsOn ? _onColors : _offColors;
            _transition.TransitionTo(colorBlock.GetColorFromState(state));
        }

        void Update()
        {
            _transition.Tick();
        }
    }
}