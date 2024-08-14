using System;
using UnityEngine;

namespace Player
{
    public class PlayerInstance : MonoBehaviour
    {
        public event Action OnPlayerIdleStart, OnPlayerIdleStop;

        [field: SerializeField] public Vignette Vignette { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform Root { get; private set; }
        [field: SerializeField] public Transform Head { get; private set; }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void MoveTo(Transform target)
        {
            Root.position = target.position;
            Root.rotation = target.rotation;
        }

        public void CallOnPlayerIdleStart() { OnPlayerIdleStart?.Invoke(); }
        public void CallOnPlayerIdleStop() {  OnPlayerIdleStop?.Invoke(); }
    }
}
