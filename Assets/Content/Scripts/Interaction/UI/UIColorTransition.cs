using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction.UI
{
    [System.Serializable]
    public class UIColorTransition
    {
        [SerializeField] private Image image;
        [SerializeField] private float duration;

        private bool inTransition;
        private float lerpValue;
        private Color fromColor;
        private Color toColor;

        public void TransitionTo (Color color)
        {
            inTransition = true;
            lerpValue = 0f;
            fromColor = image.color;
            toColor = color;
        }

        public void Tick()
        {
            if (!inTransition) return;

            lerpValue += Time.deltaTime / duration;
            image.color = Color.Lerp(fromColor, toColor, lerpValue);

            inTransition = 0f < lerpValue && lerpValue < 1f;
        }
    }
}