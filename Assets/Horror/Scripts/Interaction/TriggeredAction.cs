using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    [RequireComponent(typeof(Collider))]
    public abstract class TriggeredAction : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private bool destroyAfterTriggering = true;

        [SerializeField]
        private bool destroyEntireGameobject = false;

        private void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            PerformTriggeredAction();

            if (destroyAfterTriggering)
            {
                if (destroyEntireGameobject)
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }

        protected abstract void PerformTriggeredAction();
    }
    
    [Serializable]
    public class UnityEventTriggeredAction : UnityEvent<TriggeredAction> { }
}