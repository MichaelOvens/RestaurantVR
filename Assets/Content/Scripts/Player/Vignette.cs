using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Vignette : MonoBehaviour
    {
        public event Action OnVignetteRaiseComplete;
        public event Action OnVignetteLowerComplete;

        [field: SerializeField] public bool InTransition { get; private set; }

        [SerializeField] private Image _image;
        [SerializeField] private Color _lowered;
        [SerializeField] private Color _raised;
        [SerializeField] private float _duration;

        private float _direction = 0f;
        private float _lerpValue = 0f;

        private void Update()
        {
            if (InTransition)
            {
                _lerpValue += Time.deltaTime * _direction / _duration;
                InTransition = 0f < _lerpValue && _lerpValue < 1f;
                _lerpValue = Mathf.Clamp01(_lerpValue);
                _image.color = Color.Lerp(_lowered, _raised, _lerpValue);

                if (!InTransition)
                {
                    if (_direction > 0f) 
                        OnVignetteRaiseComplete?.Invoke();
                    else 
                        OnVignetteLowerComplete?.Invoke();
                }
            }
        }

        public void RaiseVignette(bool snapToTarget = false)
        {
            if (InTransition && _direction < 0f)
            {
                Debug.Log("Vignette interupted!");
            }

            InTransition = true;
            _direction = 1f;

            if (snapToTarget)
            {
                _lerpValue = 1f;
            }
        }

        public void LowerVignette(bool snapToTarget = false)
        {
            if (InTransition && _direction > 0f)
            {
                Debug.Log("Vignette interupted!");
            }

            InTransition = true;
            _direction = -1f;

            if (snapToTarget)
            {
                _lerpValue = 0f;
            }
        }
    }
}