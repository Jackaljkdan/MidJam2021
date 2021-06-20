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
            PefromTriggeredAction();

            if (destroyAfterTriggering)
            {
                if (destroyEntireGameobject)
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }

        protected abstract void PefromTriggeredAction();
    }
    
    [Serializable]
    public class UnityEventTriggeredAction : UnityEvent<TriggeredAction> { }
}