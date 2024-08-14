using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class ResetController : MonoBehaviour
    {
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private string _newTrialScene;

        private void Start()
        {
            PlayerManager.Instance.MoveTo(_playerPosition);
            PlayerManager.Instance.OnPlayerIdleStart += OnPlayerIdleStart;
        }

        private void OnPlayerIdleStart()
        {
            PlayerManager.Instance.OnPlayerIdleStart -= OnPlayerIdleStart;

            SceneManager.LoadScene(_newTrialScene);
        }
    }
}
