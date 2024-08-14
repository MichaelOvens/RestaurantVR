using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class RaycastInteractor : MonoBehaviour
    {
        [SerializeField] private InputAction _selectAction;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _maxRaycastDistance;

        private HashSet<Interactable> _hovered = new HashSet<Interactable>();
        private HashSet<Interactable> _pressed = new HashSet<Interactable>();

        private void Awake()
        {
            _selectAction.performed += OnPressed;
            _selectAction.canceled += OnReleased;
            _selectAction.Enable();

            _lineRenderer.useWorldSpace = false;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new Vector3[]
            {
                Vector3.zero,
                Vector3.zero,
            });
        }

        private void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            float raycastDistance = _maxRaycastDistance;
            var interactablesToStopHovering = new HashSet<Interactable>(_hovered);

            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    raycastDistance = Mathf.Min(raycastDistance, hit.distance);

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

            UpdateLineRenderer(raycastDistance);
        }

        private void UpdateLineRenderer(float distance)
        {
            if (_lineRenderer == null) return;

            _lineRenderer.SetPosition(1, Vector3.forward * distance);
        }

        private void OnPressed(InputAction.CallbackContext context)
        {
            foreach (var interactable in _hovered)
            {
                interactable.Select();
                _pressed.Add(interactable);
            }
        }

        private void OnReleased(InputAction.CallbackContext context)
        {
            foreach (var interactable in _pressed)
            {
                interactable.StopPress(this);
            }

            _pressed.Clear();
        }
    }
}
