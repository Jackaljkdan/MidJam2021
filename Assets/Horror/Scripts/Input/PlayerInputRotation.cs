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

        public Vector3 euler;

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

            euler = transform.localEulerAngles;
            euler.x += -upDownRotation * rotationSpeed;

            if (euler.x > 180)
                euler.x = Mathf.Max(360 - 89, euler.x);
            else
                euler.x = Mathf.Min(89, euler.x);

            transform.localEulerAngles = euler;
        }
    }
}