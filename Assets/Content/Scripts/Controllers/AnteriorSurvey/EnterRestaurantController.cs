using Interaction;
using Interaction.Effects;
using Player;
using Restaurant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Anterior
{
    public class EnterRestaurantController : MonoBehaviour, IOnStateMachineStateEnter
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Interactable _interactable;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private List<RestaurantVariant> _restaurants;

        [Header("Dimming")]
        [SerializeField] private float _duration;
        [SerializeField] private Light _light;
        [SerializeField] private float _lightStartIntensity;
        [SerializeField] private float _lightEndIntensity;
        [SerializeField] private MeshRenderer _skyRenderer;
        [SerializeField] private Color _skyStartColor;
        [SerializeField] private Color _skyEndColor;

        [System.Serializable]
        private class RestaurantVariant
        {
            [field: SerializeField] public TrialConditions.MenuCondition Condition { get; private set; }
            [field: SerializeField] public string SceneName { get; private set; }
        }

        private void Awake()
        {
            _interactable.IsInteractable = false;
            _particleSystem.Stop();
        }

        public void StartRestaurantTransition()
        {
            _animator.SetTrigger("openDoor");
            StartCoroutine(DimRoutine());
        }

        private IEnumerator DimRoutine()
        {
            float lerp = 0f;

            while (lerp < 1f)
            {
                lerp += Time.deltaTime / _duration;

                _light.intensity = Mathf.Lerp(_lightStartIntensity, _lightEndIntensity, lerp);
                _skyRenderer.material.color = Color.Lerp(_skyStartColor, _skyEndColor, lerp);

                yield return null;
            }
        }

        public void OnStateMachineStateEnter(string label)
        {
            // Triggered when the animation is finished playing
            _interactable.OnSelected += OnInteractableSelected;
            _interactable.IsInteractable = true;
            _particleSystem.Play();
        }

        private void OnInteractableSelected(object sender, EventArgs e)
        {
            _interactable.OnSelected -= OnInteractableSelected;

            PlayerManager.Instance.Vignette.RaiseVignette();
            PlayerManager.Instance.Vignette.OnVignetteRaiseComplete += OnVignetteRaised;
        }

        private void OnVignetteRaised()
        {
            PlayerManager.Instance.Vignette.OnVignetteRaiseComplete -= OnVignetteRaised;

            string restaurantScene = _restaurants.First(i => i.Condition == TrialManager.Conditions.MenuType).SceneName;
            SceneManager.LoadScene(restaurantScene);
        }
    }
}
