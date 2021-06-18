using JK.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    [RequireComponent(typeof(Animator))]
    public class DoorInteractable : InteractableBehaviour
    {
        #region Inspector

        [SerializeField]
        private bool _isOpen = false;

        [SerializeField]
        private Collider doorCollider = null;

        #endregion

        public bool IsOpen
        {
            get => _isOpen;
            private set => _isOpen = value;
        }

        public bool IsAnimating { get; private set; }

        protected override void PerformInteraction(RaycastHit hit)
        {
            if (IsAnimating)
                return;

            if (!IsOpen)
                GetComponent<Animator>().Play("DoorOpen");
            else
                GetComponent<Animator>().Play("DoorClose");

            Vector3 localNormal = hit.collider.transform.InverseTransformDirection(hit.normal);
            if (localNormal.z < 0 && !IsOpen)
                doorCollider.enabled = false;
            else if (localNormal.z > 0 && IsOpen)
                doorCollider.enabled = false;

            IsAnimating = true;
        }

        public void OnDoorOpened()
        {
            IsAnimating = false;
            IsOpen = true;
            doorCollider.enabled = true;
        }

        public void OnDoorClosed()
        {
            IsAnimating = false;
            IsOpen = false;
            doorCollider.enabled = true;
        }
    }
}