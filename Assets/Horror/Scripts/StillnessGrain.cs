using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Horror
{
    public class StillnessGrain : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float maxStillnessSeconds = 10f;

        [SerializeField]
        private float falloffMultiplier = 2;

        #endregion

        [Inject]
        private PostProcessVolume volume = null;

        private void Start()
        {
            StartCoroutine(StillnessCoroutine());
        }

        private IEnumerator StillnessCoroutine()
        {
            Vector3 lastPosition = transform.position;
            float elapsedSecondsWhileStill = 0;
            var grain = volume.profile.GetSetting<Grain>();

            while (true)
            {
                yield return null;

                Vector3 currentPosition = transform.position;

                if ((currentPosition - lastPosition).sqrMagnitude > 0)
                    elapsedSecondsWhileStill = Mathf.Max(0, elapsedSecondsWhileStill - Time.deltaTime*2);
                else
                    elapsedSecondsWhileStill += Time.deltaTime;

                grain.intensity.value = elapsedSecondsWhileStill / maxStillnessSeconds;

                lastPosition = currentPosition;
            }
        }
    }
    
    [Serializable]
    public class UnityEventStillnessGrain : UnityEvent<StillnessGrain> { }
}