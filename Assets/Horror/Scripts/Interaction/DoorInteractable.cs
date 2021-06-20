using JK.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        private bool _isLocked = false;

        [SerializeField]
        private Collider doorCollider = null;

        [SerializeField]
        private Collider passageCollider = null;

        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip openClip = null;

        [SerializeField]
        private AudioClip closeClip = null;

        [SerializeField]
        private AudioClip lockedClip = null;

        #endregion

        public bool IsOpen
        {
            get => _isOpen;
            private set => _isOpen = value;
        }

        public bool IsLocked
        {
            get => _isLocked;
            set => _isLocked = value;
        }

        public bool IsAnimating { get; private set; }

        public GameObject MovingPiece => doorCollider.gameObject;

        protected override void PerformInteraction(RaycastHit hit)
        {
            if (IsAnimating)
                return;

            if (IsLocked)
            {
                if (!audioSource.isPlaying)
                    audioSource.PlayOneShot(lockedClip);

                return;
            }

            if (!IsOpen)
            {
                GetComponent<Animator>().Play("DoorOpen");
                audioSource.PlayOneShot(openClip);
            }
            else
            {
                GetComponent<Animator>().Play("DoorClose");
                audioSource.PlayOneShot(closeClip);
            }

            if (hit.collider != null)
            {
                Vector3 localNormal = hit.collider.transform.InverseTransformDirection(hit.normal);
                if (localNormal.z < 0 && !IsOpen)
                    doorCollider.enabled = false;
                else if (localNormal.z > 0 && IsOpen)
                    doorCollider.enabled = false;
            }
            else
            {
                doorCollider.enabled = false;
            }

            IsAnimating = true;
        }

        public void ForceClose(bool disableCollider)
        {
            if (IsAnimating)
            {
                if (IsOpen)
                    return;
            }
            else if (!IsOpen)
            {
                return;
            }

            GetComponent<Animator>().Play("DoorClose");
            audioSource.PlayOneShot(closeClip);
            passageCollider.gameObject.SetActive(true);

            if (disableCollider)
                doorCollider.enabled = false;
        }

        public void OnDoorOpened()
        {
            IsAnimating = false;
            IsOpen = true;
            doorCollider.enabled = true;
            passageCollider.gameObject.SetActive(false);
        }

        public void OnDoorClosed()
        {
            IsAnimating = false;
            IsOpen = false;
            doorCollider.enabled = true;
            passageCollider.gameObject.SetActive(false);
        }
    }
}