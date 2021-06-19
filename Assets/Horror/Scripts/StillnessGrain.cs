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
        private float movementDeltaSquared = 0.5f;

        [SerializeField]
        private float lerp = 5f;

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
            float positionUpdateSeconds = 0.2f;
            float elapsedSecondsSincePositionUpdate = 0;
            float elapsedSecondsWhileStill = 0;
            var grain = volume.profile.GetSetting<Grain>();

            while (true)
            {
                yield return null;

                Vector3 currentPosition = transform.position;

                if ((currentPosition - lastPosition).sqrMagnitude <= movementDeltaSquared)
                    elapsedSecondsWhileStill += Time.deltaTime;
                else
                    elapsedSecondsWhileStill = 0;

                grain.intensity.value = Mathf.Lerp(grain.intensity.value, elapsedSecondsWhileStill / maxStillnessSeconds, lerp * Time.deltaTime);

                elapsedSecondsSincePositionUpdate += Time.deltaTime;

                if (elapsedSecondsSincePositionUpdate >= positionUpdateSeconds)
                {
                    elapsedSecondsSincePositionUpdate = 0;
                    lastPosition = currentPosition;
                }
            }
        }
    }
    
    [Serializable]
    public class UnityEventStillnessGrain : UnityEvent<StillnessGrain> { }
}