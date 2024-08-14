using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Interaction;
using UnityEngine.Events;

namespace Movement
{
    public class MovementNode : MonoBehaviour
    {
        public EventHandler<MovementNode> OnNodeSelected;
        public UnityEvent OnNodeEntered;

        [field:SerializeField] public Transform Target { get; private set; }
        public ReadOnlyCollection<MovementNode> LinkedNodes => _linkedNodes.AsReadOnly();
        
        [SerializeField] private List<MovementNode> _linkedNodes;
        [SerializeField] private Interactable _interactable;

        private void Awake()
        {
            _interactable.OnSelected += OnSelected;
        }

        private void OnSelected(object sender, EventArgs e)
        {
            OnNodeSelected?.Invoke(this, this);
        }

        public void SetNodeVisibility(bool isActive)
        {
            gameObject.SetActive(isActive);
            _interactable.IsInteractable = isActive;
        }
    }
}