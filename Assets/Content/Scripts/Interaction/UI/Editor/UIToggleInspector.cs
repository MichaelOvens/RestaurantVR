using UnityEditor;

namespace Interaction.UI.Editors
{
    [CustomEditor(typeof(UIToggle))]
    public class UIToggleInspector : Editor
    {
        private UIToggleGroup previousToggleGroupMember;
        private UIToggleGroup previousToggleGroupExclusive;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var toggle = target as UIToggle;

            if (toggle.ToggleGroupMember != previousToggleGroupMember)
            {
                previousToggleGroupMember?.DeregisterMember(toggle);
                toggle.ToggleGroupMember = toggle.ToggleGroupMember;
                previousToggleGroupMember = toggle.ToggleGroupMember;
            }

            if (toggle.ToggleGroupExclusive != previousToggleGroupExclusive)
            {
                previousToggleGroupExclusive?.DeregisterExclusive(toggle);
                toggle.ToggleGroupExclusive = toggle.ToggleGroupExclusive;
                previousToggleGroupExclusive = toggle.ToggleGroupExclusive;
            }
        }
    }
}