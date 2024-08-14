using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private float _transition = 0.1f;
        [SerializeField] private List<AudioSource> _sources;

        private bool _isPlaying = false;

        private void Awake()
        {
            foreach (var source in _sources)
            {
                source.volume = 0f;
            }
        }

        public void Play()
        {
            _isPlaying = true;
        }

        private void Update()
        {
            foreach (var source in _sources)
            {
                float direction = _isPlaying ? 1f : -1f;
                float volume = source.volume += direction * Time.deltaTime / _transition;
                volume = Mathf.Clamp01(volume);
                source.volume = volume;
            }
        }
    }
}
