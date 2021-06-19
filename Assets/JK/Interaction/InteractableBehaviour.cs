using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JK.Interaction
{
    public abstract class InteractableBehaviour : MonoBehaviour, IInteractable
    {
        #region Inspector

        

        #endregion

        // declare a start method so that the monobehaviour can be disabled in in the inspector if needed
        protected virtual void Start()
        {

        }

        public void Interact(RaycastHit hit)
        {
            if (enabled)
                PerformInteraction(hit);
        }

        protected abstract void PerformInteraction(RaycastHit hit);
    }
    
    [Serializable]
    public class UnityEventInteractableBehaviour : UnityEvent<InteractableBehaviour> { }
}