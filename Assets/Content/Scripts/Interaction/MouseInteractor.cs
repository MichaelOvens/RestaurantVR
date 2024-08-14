using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class MouseInteractor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private InputAction _selectAction;

        private HashSet<Interactable> _hovered = new HashSet<Interactable>();
        private HashSet<Interactable> _pressed = new HashSet<Interactable>();

        private void Awake()
        {
            _selectAction.performed += OnPressed;
            _selectAction.canceled += OnReleased;
            _selectAction.Enable();
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            var interactablesToStopHovering = new HashSet<Interactable>(_hovered);

            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    interactablesToStopHovering.Remove(interactable);

                    if (!interactable.IsHovered)
                    {
                        _hovered.Add(interactable);
                        interactable.StartHover(null);
                    }
                }
            }

            foreach (var interactable in interactablesToStopHovering)
            {
                _hovered.Remove(interactable);
                interactable.StopHover(null);
            }
        }

        private void OnPressed(InputAction.CallbackContext context)
        {
            foreach (var interactable in _hovered)
            {
                _pressed.Add(interactable);
            }
        }

        private void OnReleased(InputAction.CallbackContext context)
        {
            foreach (var interactable in _pressed)
            {
                if (_hovered.Contains(interactable))
                {
                    interactable.Select();
                }
            }

            foreach (var interactable in _pressed)
            {
                interactable.StopPress(this);
            }

            _pressed.Clear();
        }
    }
}
