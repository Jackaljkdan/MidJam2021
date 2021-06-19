using JK.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    public class LightSwitchInteractable : InteractableBehaviour
    {
        #region Inspector

        [SerializeField]
        private Light lightTarget = null;

        [SerializeField]
        private bool startsOff = false;

        #endregion

        private float onIntensity;

        private void Start()
        {
            onIntensity = lightTarget.intensity;
        }

        protected override void PerformInteraction(RaycastHit hit)
        {
            if (lightTarget.intensity == onIntensity)
                lightTarget.intensity = 0;
            else
                lightTarget.intensity = onIntensity;
        }
    }
}