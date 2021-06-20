using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class LookAwayOnEnabled : MonoBehaviour
    {
        #region Inspector



        #endregion

        [Inject(Id = "player")]
        private Transform playerTransform = null;

        private void OnEnable()
        {
            Vector3 direction = transform.position - playerTransform.position;
            direction.y = 0;

            transform.forward = direction;
        }
    }
    
    [Serializable]
    public class UnityEventLookAwayOnEnabled : UnityEvent<LookAwayOnEnabled> { }
}