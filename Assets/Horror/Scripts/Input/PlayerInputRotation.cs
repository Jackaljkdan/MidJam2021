using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    public class PlayerInputRotation : MonoBehaviour
    {
        #region Inspector fields

        public float rotationSpeed;

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            float leftRightRotation = UnityEngine.Input.GetAxis("Mouse X");
            float upDownRotation = UnityEngine.Input.GetAxis("Mouse Y");

            transform.RotateAround(transform.position, Vector3.up, leftRightRotation * rotationSpeed);
            transform.RotateAround(transform.position, transform.right, - upDownRotation * rotationSpeed);
        }
    }
}