using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction.Effects
{
    public class HoverColor : HoverInteractable
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hoveredColor;

        protected override void OnTransitionUpdate(float lerpValue)
        {
            _image.color = Color.Lerp(_defaultColor, _hoveredColor, lerpValue);
        }
    }
}
