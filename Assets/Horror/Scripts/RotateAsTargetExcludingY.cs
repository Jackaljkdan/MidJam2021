using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    public class RotateAsTargetExcludingY : MonoBehaviour
    {
        #region Inspector

        public Transform target;

        #endregion

        private void Update()
        {
            Vector3 forward = target.forward;
            forward.y = 0;

            transform.forward = forward;
        }
    }
    
    [Serializable]
    public class UnityEventRotateAsTargetExcludingY : UnityEvent<RotateAsTargetExcludingY> { }
}