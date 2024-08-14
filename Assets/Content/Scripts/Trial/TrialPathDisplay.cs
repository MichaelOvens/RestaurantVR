using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trial
{
    public class TrialPathDisplay : MonoBehaviour
    {
        [SerializeField] private InputAction _displayAction;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private TextMeshProUGUI _textField;

        private void Awake()
        {
            _displayAction.performed += ToggleDisplay;
            _displayAction.Enable();

            _textField.text = TrialData.OutputFolder;
        }

        private void ToggleDisplay(InputAction.CallbackContext context)
        {
            _gameObject.SetActive(!_gameObject.activeSelf);
        }
    }
}
