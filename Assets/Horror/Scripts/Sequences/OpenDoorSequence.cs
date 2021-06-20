using Horror.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Sequences
{
    public class OpenDoorSequence : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private LightSwitchInteractable interactable = null;

        [SerializeField]
        private DoorInteractable door = null;

        [SerializeField]
        private int interactionsToOpen = 3;

        #endregion

        private int interactionsCount = 0;

        private void Start()
        {
            door.IsLocked = true;
            interactable.onInteraction.AddListener(OnInteraction);
        }

        private void OnDestroy()
        {
            if (interactable)
                interactable.onInteraction.RemoveListener(OnInteraction);
        }

        private void OnInteraction()
        {
            interactionsCount++;

            if (interactionsCount == interactionsToOpen)
            {
                door.IsLocked = false;
                door.Interact(new RaycastHit());
                interactable.onInteraction.RemoveListener(OnInteraction);
                Destroy(this);
            }
        }
    }
    
    [Serializable]
    public class UnityEventOpenDoorSequence : UnityEvent<OpenDoorSequence> { }
}