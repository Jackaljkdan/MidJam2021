using JK.Events;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Input
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInputRigidBody : MonoBehaviour, IMovementInput
    {
        #region Inspector

        [SerializeField]
        private float _speed = 3;

        public float acceleration = 300;

        public Transform directionReference;

        //[SerializeField, ReadOnly]
        //private double currentSpeed = 0;

        public UnityEventVector3 _onInput = new UnityEventVector3();

        private void Reset()
        {
            directionReference = transform;

            var body = GetComponent<Rigidbody>();
            body.isKinematic = false;
        }

        #endregion

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public UnityEventVector3 onInput => _onInput;

        private void Update()
        {
            var body = GetComponent<Rigidbody>();

            Vector3 input = new Vector3(
                UnityEngine.Input.GetAxis("Horizontal"),
                0,
                UnityEngine.Input.GetAxis("Vertical")
            );

            foreach (var touch in UnityEngine.Input.touches)
            {
                if (touch.position.x < Screen.width / 2.0)
                {
                    input = new Vector3(0, 0, 1);
                    break;
                }
            }

            Vector3 directionedInput = directionReference.TransformDirection(input);
            directionedInput.y = 0;
            directionedInput = Vector3.ClampMagnitude(directionedInput, 1);

            float multiplier = Mathf.Lerp(acceleration, 0, body.velocity.sqrMagnitude / (Speed * Speed));
            Vector3 accelerationForce = directionedInput * multiplier;

            body.AddForce(accelerationForce, ForceMode.Acceleration);

            //currentSpeed = Math.Round((currentSpeed * 5 + body.velocity.magnitude) / 6, 1);

            onInput.Invoke(input);
        }
    }
    
    [Serializable]
    public class UnityEventPlayerInputRigidBody : UnityEvent<PlayerInputRigidBody> { }
}