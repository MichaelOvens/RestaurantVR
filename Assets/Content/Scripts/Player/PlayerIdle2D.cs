using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerIdle2D : MonoBehaviour
    {
        [SerializeField] private PlayerInstance _player;
        [SerializeField] private InputAction _action;
        [SerializeField] private bool _isIdle;

        private void Awake()
        {
            _action.performed += OnToggleIdle;
            _action.Enable();
        }

        private void OnToggleIdle(InputAction.CallbackContext context)
        {
            _isIdle = !_isIdle;

            if (_isIdle) _player.CallOnPlayerIdleStart();
            else _player.CallOnPlayerIdleStop();
        }
    }
}
