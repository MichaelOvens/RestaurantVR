using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class HoverLight : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable;

        [SerializeField] private Light _light;
        [SerializeField] private float _maxIntensity;
        [SerializeField] private float _minIntensity;
        [SerializeField] private float _transitionDuration;

        private bool _isOn;
        private bool _inTransition;

        private void Awake()
        {
            _interactable.OnHoverStart += OnHoverStart;
            _interactable.OnHoverStop += OnHoverStop;

            _light.intensity = _minIntensity;
            _isOn = false;
            _inTransition = false;
        }

        private void OnHoverStart(object sender, EventArgs e)
        {
            _isOn = true;
            _inTransition = true;
        }

        private void OnHoverStop(object sender, EventArgs e)
        {
            _isOn = false;
            _inTransition = true;
        }

        private void Update()
        {
            if (_inTransition)
            {
                float targetIntensity = _isOn ? _maxIntensity : _minIntensity;
                float delta = (_maxIntensity - _minIntensity) * Time.deltaTime / _transitionDuration;

                _light.intensity = Mathf.MoveTowards(_light.intensity, targetIntensity, delta);

                _inTransition = !Mathf.Approximately(_light.intensity, targetIntensity);
            }
        }
    }
}
