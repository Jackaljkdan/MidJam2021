using JK.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    public class StillnessMeter : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float maxStillnessSeconds = 10f;

        [SerializeField]
        private float falloffMultiplier = 2;

        public UnityEventFloat onStillnessMeasure = new UnityEventFloat();

        #endregion

        Vector3 lastPosition;
        float elapsedSecondsWhileStill = 0;

        private void Start()
        {
            lastPosition = transform.position;
        }

        private void Update()
        {
            Vector3 currentPosition = transform.position;

            if ((currentPosition - lastPosition).sqrMagnitude > 0)
                elapsedSecondsWhileStill = Mathf.Max(0, elapsedSecondsWhileStill - Time.deltaTime * falloffMultiplier);
            else
                elapsedSecondsWhileStill = Mathf.Min(maxStillnessSeconds, elapsedSecondsWhileStill + Time.deltaTime);

            onStillnessMeasure.Invoke(elapsedSecondsWhileStill / maxStillnessSeconds);

            lastPosition = currentPosition;
        }
    }
    
    [Serializable]
    public class UnityEventStillnessMeter : UnityEvent<StillnessMeter> { }
}