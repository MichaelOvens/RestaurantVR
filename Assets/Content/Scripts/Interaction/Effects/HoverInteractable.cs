using System;
using UnityEngine;

namespace Interaction.Effects
{
    public abstract class HoverInteractable : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable;

        [SerializeField] private float _transitionDuration;

        private bool _isOn;
        private bool _inTransition;
        private float _lerpValue;

        protected abstract void OnTransitionUpdate(float lerpValue);

        protected virtual void Awake()
        {
            _interactable.OnHoverStart += OnHoverStart;
            _interactable.OnHoverStop += OnHoverStop;

            _isOn = false;
            _inTransition = false;
            _lerpValue = 0f;

            OnTransitionUpdate(_lerpValue);
        }

        private void OnHoverStart(object sender, EventArgs e)
        {
            _isOn = true;
            _inTransition = true;
        }

        private void OnHoverStop(object sender, EventArgs e)
        {
            _isOn = false;
            _inTransition = true;
        }

        private void Update()
        {
            if (_inTransition)
            {
                float direction = _isOn ? 1 : -1;
                _lerpValue += Time.deltaTime * direction / _transitionDuration;
                _inTransition = 0f < _lerpValue && _lerpValue < 1f;
                _lerpValue = Mathf.Clamp01(_lerpValue);
                OnTransitionUpdate(_lerpValue);
            }
        }
    }
}
