using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class ForceCameraLook : MonoBehaviour
    {
        #region Inspector

        public float lerp = 2;

        #endregion

        public class Factory : PlaceholderFactory<ForceCameraLook> { }
        
        [Inject(Id = "player.camera")]
        private Transform cameraTransform = null;

        private void LateUpdate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - cameraTransform.position, Vector3.up);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, lerp * Time.deltaTime);
        }

    }
    
    [Serializable]
    public class UnityEventForceCameraLook : UnityEvent<ForceCameraLook> { }
}