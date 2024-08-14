using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction.UI
{
    public class UIToggleGroup : MonoBehaviour
    {
        public bool AllowMultipleMembersToBeSwitchedOn;

        private HashSet<UIToggle> members = new HashSet<UIToggle>();

        // This group cannot have any members switched on if an exclusive is switched on,
        // and no exclusive can be switched on if any members are switched on
        private HashSet<UIToggle> exclusives = new HashSet<UIToggle>();

        public void RegisterMember(UIToggle toggle)
        {
            members.Add(toggle);
            toggle.OnValueChanged += MemberStateChanged;
        }

        public void DeregisterMember(UIToggle toggle)
        {
            members.Remove(toggle);
            toggle.OnValueChanged -= MemberStateChanged;
        }

        public void RegisterExclusive(UIToggle toggle)
        {
            exclusives.Add(toggle);
            toggle.OnValueChanged += ExclusiveStateChanged;
        }

        public void DeregisterExclusive(UIToggle toggle)
        {
            exclusives.Remove(toggle);
        }

        private void MemberStateChanged(object sender, bool isSelected)
        {
            UIToggle modifiedToggle = sender as UIToggle;

            if (isSelected)
            {
                if (!AllowMultipleMembersToBeSwitchedOn)
                {
                    foreach (var toggle in members)
                    {
                        if (toggle != modifiedToggle)
                        {
                            toggle.IsOn = false;
                        }
                    }
                }

                foreach (var toggle in exclusives)
                {
                    toggle.IsOn = false;
                }
            }
        }

        private void ExclusiveStateChanged(object sender, bool isSelected)
        {
            if (isSelected)
            {
                foreach (var toggle in members)
                {
                    toggle.IsOn = false;
                }
            }
        }
    }
}