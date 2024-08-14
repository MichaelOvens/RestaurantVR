using Movement;
using Restaurant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MenuSurvey
{
    [System.Serializable]
    public class MenuSpawner
    {
        public event Action<Menu> OnMenuSpawned;

        [SerializeField] private Menu _prefabMenu;
        [SerializeField] private MovementManager _movement;
        [SerializeField] private List<SpawnLocation> spawnLocations;

        [System.Serializable]
        private class SpawnLocation
        {
            public MovementNode node;
            public Transform parent;
        }

        private SpawnLocation _selectedLocation = null;

        public void StartListeningForTrigger()
        {
            foreach (var location in spawnLocations)
            {
                location.node.OnNodeSelected += OnNodeSelected;
            }
        }

        private void OnNodeSelected(object sender, MovementNode node)
        {
            foreach (var location in spawnLocations)
            {
                location.node.OnNodeSelected -= OnNodeSelected;
            }

            _selectedLocation = spawnLocations.First(i => i.node == node);
            _movement.OnMoveInProgress += OnMoveInProgress;
        }

        private void OnMoveInProgress(object sender, MovementNode node)
        {
            _movement.OnMoveInProgress -= OnMoveInProgress;

            Menu menu = GameObject.Instantiate(_prefabMenu, _selectedLocation.parent);
            OnMenuSpawned.Invoke(menu);
        }
    }
}
