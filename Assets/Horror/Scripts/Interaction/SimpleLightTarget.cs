using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    public class SimpleLightTarget : LightTargetBehaviour
    {
        #region Inspector

        [SerializeField]
        private Light target = null;

        [SerializeField]
        private bool startsOff = false;

        #endregion

        private float onIntensity;

        private void Start()
        {
            onIntensity = target.intensity;
        }

        public override void Toggle()
        {
            if (target.intensity == onIntensity)
                target.intensity = 0;
            else
                target.intensity = onIntensity;
        }
    }
    
    [Serializable]
    public class UnityEventSimpleLightTarget : UnityEvent<SimpleLightTarget> { }
}