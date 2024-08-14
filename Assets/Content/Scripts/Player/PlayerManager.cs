using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerInstance Instance { get; private set; }
        private static PlayerManager _managerInstance = null;

        public static bool IsIn2dMode => _managerInstance._use2dPlayer;

        [SerializeField] private bool _use2dPlayer;
        [SerializeField] private PlayerInstance _2dInstance;
        [SerializeField] private PlayerInstance _xrInstance;
        [SerializeField] private InputAction _exitAction;

        [Header("Bootstrapping")]
        [SerializeField] private string _bootstrapScene;
        [SerializeField] private string _firstScene;

        private void Awake()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (_managerInstance != null)
            {
                Destroy(gameObject);
                return;
            }

            _managerInstance = this;
            DontDestroyOnLoad(gameObject);

            _2dInstance.SetActive(IsIn2dMode);
            _xrInstance.SetActive(!IsIn2dMode);
            Instance = IsIn2dMode ? _2dInstance : _xrInstance;

            Debug.Log($"Player instance set to {Instance.name}");

            _exitAction.performed += OnExit;
            _exitAction.Enable();

            if (SceneManager.GetActiveScene().name == _bootstrapScene)
            {
                SceneManager.LoadScene(_firstScene);
            }
        }

        private void OnExit(InputAction.CallbackContext context)
        {
            Application.Quit();
        }
    }
}
