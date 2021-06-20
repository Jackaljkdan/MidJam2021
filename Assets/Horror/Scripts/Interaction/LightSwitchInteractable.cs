using JK.Interaction;
using MyBox;
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
        private LightTargetBehaviour lightTarget = null;

        [SerializeField]
        private AudioClip audioClip = null;

        [SerializeField]
        private ParticlesWhenLightOff particles = null;

        #endregion

        protected override void Start()
        {
            base.Start();

            if (particles && lightTarget)
                particles.target = lightTarget.Light;
        }

        protected override void PerformInteraction(RaycastHit hit)
        {
            if (lightTarget)
                lightTarget.Toggle();

            gameObject.GetOrAddComponent<AudioSource>().PlayOneShot(audioClip);
        }
    }
}