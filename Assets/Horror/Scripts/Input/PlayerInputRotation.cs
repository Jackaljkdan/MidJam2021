using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Horror
{
    public class PlayerInputRotation : MonoBehaviour
    {
        #region Inspector fields

        public float rotationSpeed;

        public float touchMultiplier = 0.1f;

        public Vector3 euler;

        #endregion

        [Inject(Id = "dbg")]
        private Text dbgText = null;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            float leftRightRotation;
            float upDownRotation;

            if (!Application.isMobilePlatform)
            {
                leftRightRotation = UnityEngine.Input.GetAxis("Mouse X");
                upDownRotation = UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                leftRightRotation = 0;
                upDownRotation = 0;

                float halfScreenWidth = Screen.width / 2.0f;

                dbgText.text = $"{Screen.width} half {halfScreenWidth}\n";

                foreach (var touch in UnityEngine.Input.touches)
                {
                    dbgText.text += $"{touch.phase} {touch.position.x:0} > h {touch.position.x > halfScreenWidth}\n";

                    if (touch.phase != TouchPhase.Moved)
                        continue;

                    if (touch.position.x > halfScreenWidth)
                    {
                        dbgText.text += $"am i a clown?\n";

                        leftRightRotation += touch.deltaPosition.x * touchMultiplier;
                        upDownRotation += touch.deltaPosition.y * touchMultiplier;
                    }
                }
            }

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