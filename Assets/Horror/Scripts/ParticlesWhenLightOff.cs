using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlesWhenLightOff : MonoBehaviour
    {
        #region Inspector

        public Light target;

        #endregion

        private void Update()
        {
            if (target == null)
                return;

            var particles = GetComponent<ParticleSystem>();

            if (Mathf.Approximately(target.intensity, 0))
            {
                if (!particles.isPlaying)
                {
                    //Debug.Log("playing particles");
                    particles.Play();
                }
            }
            else if (particles.isPlaying)
            {
                //Debug.Log("stop playing particles");
                particles.Stop();
            }
        }
    }
}