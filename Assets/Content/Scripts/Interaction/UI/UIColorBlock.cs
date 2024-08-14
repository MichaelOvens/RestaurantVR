using UnityEngine;

namespace Interaction.UI
{
    [System.Serializable]
    public class UIColorBlock
    {
        public Color Default;
        public Color Hovered;
        public Color Pressed;
        public Color Disabled;

        public Color GetColorFromState (UIControlState state)
        {
            switch (state)
            {
                case UIControlState.Hovered:
                    return Hovered;
                case UIControlState.Pressed:
                    return Pressed;
                case UIControlState.Disabled:
                    return Disabled;
                default:
                    return Default;
            }
        }
    }
}