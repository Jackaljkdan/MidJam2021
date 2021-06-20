using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class LookAtPlayer : MonoBehaviour
    {
        #region Inspector

        

        #endregion

        [Inject(Id = "player")]
        private Transform playerTransform = null;

        private void Update()
        {
            transform.LookAt(playerTransform);
        }
    }
    
    [Serializable]
    public class UnityEventLookAtPlayer : UnityEvent<LookAtPlayer> { }
}