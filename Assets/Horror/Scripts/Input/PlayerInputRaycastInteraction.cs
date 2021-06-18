using DG.Tweening;
using JK.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Input
{
    public class PlayerInputRaycastInteraction : MonoBehaviour
    {
        #region Inspector

        public LayerMask layerMask = ~0;

        public float distance = 5;

        #endregion

        private bool isFiring = false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetAxis("Fire1") != 1)
            {
                isFiring = false;
                return;
            }

            if (isFiring)
                return;

            isFiring = true;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance, layerMask))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                    interactable.Interact(hit);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        }
    }
}