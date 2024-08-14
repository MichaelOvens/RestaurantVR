using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class MovementManager : MonoBehaviour
    {
        public EventHandler<MovementNode> OnMoveStarted;
        public EventHandler<MovementNode> OnMoveInProgress;
        public EventHandler<MovementNode> OnMoveCompleted;

        private PlayerInstance Player => PlayerManager.Instance;
        private IEnumerator _currentMove = null;
        [SerializeField] private List<MovementNode> _activeNodes;

        private void Awake()
        {
            var allNodes = FindObjectsOfType<MovementNode>(true);
            foreach (var node in allNodes)
            {
                node.OnNodeSelected += OnNodeSelected;
            }
        }

        private void OnNodeSelected(object sender, MovementNode node)
        {
            if (_currentMove != null)
            {
                Debug.Log("Move command blocked by active movement");
                return;
            }

            _currentMove = DoMovement(node);
            StartCoroutine(_currentMove);
        }

        private IEnumerator DoMovement(MovementNode node)
        {
            OnMoveStarted?.Invoke(this, node);

            Player.Vignette.RaiseVignette();
            while (Player.Vignette.InTransition)
            {
                yield return null;
            }

            SetActiveNodeVisibility(false);
            MoveToNode(node);
            node.OnNodeEntered.Invoke();
            SetActiveNodeVisibility(true);

            Player.Vignette.LowerVignette();
            while (Player.Vignette.InTransition)
            {
                yield return null;
            }

            _currentMove = null;
         
            OnMoveCompleted?.Invoke(this, node);
        }

        private void SetActiveNodeVisibility(bool isActive)
        {
            foreach (var node in _activeNodes)
            {
                node.SetNodeVisibility(isActive);
            }
        }

        private void MoveToNode (MovementNode node)
        {
            Player.MoveTo(node.Target);

            _activeNodes.Clear();
            _activeNodes.AddRange(node.LinkedNodes);

            OnMoveInProgress?.Invoke(this, node);
        }
    }
}