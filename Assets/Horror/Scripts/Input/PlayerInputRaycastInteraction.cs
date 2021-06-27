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

        public float mobileMaxTouchSeconds = 0.2f;

        #endregion

        private bool hasAlreadyFired = false;

        private float touchSeconds;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!Application.isMobilePlatform)
                DesktopUpdate();
            else
                MobileUpdate();
        }

        private void DesktopUpdate()
        {
            if (!hasAlreadyFired)
            {
                if (UnityEngine.Input.GetAxis("Fire1") == 1)
                {
                    Raycast();
                    hasAlreadyFired = true;
                }
            }
            else if (UnityEngine.Input.GetAxis("Fire1") != 1)
            {
                hasAlreadyFired = false;
            }
        }

        private void MobileUpdate()
        {
            if (UnityEngine.Input.touches.Length == 0)
            {
                touchSeconds = 0;
                hasAlreadyFired = false;
                return;
            }

            if (hasAlreadyFired)
                return;

            touchSeconds += Time.deltaTime;

            if (touchSeconds < mobileMaxTouchSeconds
                && UnityEngine.Input.touches.Length == 1
                && UnityEngine.Input.touches[0].phase == TouchPhase.Ended
                && UnityEngine.Input.touches[0].position.x > Screen.width / 2.0f
                )
            {
                Raycast();
                hasAlreadyFired = true;
            }
        }

        private void Raycast()
        {
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