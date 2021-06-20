using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    public abstract class StayTriggerAction : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private bool destroyAfterTriggering = true;

        [SerializeField]
        private bool destroyEntireGameobject = false;

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (CanPerformAction())
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
        }

        protected abstract bool CanPerformAction();

        protected abstract void PerformTriggeredAction();
    }

    [Serializable]
    public class UnityEventStayTriggerAction : UnityEvent<StayTriggerAction> { }
}