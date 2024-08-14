using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    [DisallowMultipleComponent]
    public class Interactable : MonoBehaviour
    {
        public EventHandler OnInteractableStart, OnInteractableStop, OnHoverStart, OnHoverStop, OnPressStart, OnPressStop, OnSelected;

        public bool IsInteractable
        {
            get { return GetInteractableState(); }
            set { SetInteractableState(value); }
        }
        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }

        [SerializeField] private bool _isInteractable = true;
        
        private HashSet<object> _hoveredBy = new HashSet<object>();
        private HashSet<object> _pressedBy = new HashSet<object>();

        public void StartHover(object interactor)
        {
            _hoveredBy.Add(interactor);
            
            if (!IsInteractable) return;

            if (_hoveredBy.Count == 1)
            {
                IsHovered = true;
                OnHoverStart?.Invoke(this, null);
            }
        }

        public void StopHover(object interactor)
        {
            _hoveredBy.Remove(interactor);
            
            if (!IsInteractable) return;

            if (_hoveredBy.Count == 0)
            {
                IsHovered = false;
                OnHoverStop?.Invoke(this, null);
            }
        }

        public void StartPress(object interactor)
        {
            _pressedBy.Add(interactor);

            if (!IsInteractable) return;

            if (_pressedBy.Count == 1)
            {
                IsPressed = true;
                OnPressStart?.Invoke(this, null);
            }
        }

        public void StopPress(object interactor)
        {
            _pressedBy.Remove(interactor);

            if (!IsInteractable) return;

            if (_pressedBy.Count == 0)
            {
                IsPressed = false;
                OnPressStop?.Invoke(this, null);
            }
        }

        public void Select()
        {
            if (!IsInteractable) return;

            OnSelected?.Invoke(this, null);
        }

        private void SetInteractableState(bool value)
        {
            bool _previousValue = IsInteractable;
            _isInteractable = value;

            if (IsInteractable != _previousValue)
            {
                if (IsInteractable)
                    OnInteractableStart?.Invoke(this, null);
                else 
                    OnInteractableStop?.Invoke(this, null);
            }

            if (!IsInteractable)
            {
                if (IsHovered)
                {
                    IsHovered = false;
                    OnHoverStop?.Invoke(this, null);
                }
                if (IsPressed)
                {
                    IsPressed = false;
                    OnPressStop?.Invoke(this, null);
                }
            }
        }

        private bool GetInteractableState()
        {
            return _isInteractable;
        }
    }
}
