using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Input
{
    [RequireComponent(typeof(IMovementInput))]
    public class PlayerInputFootsteps : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float secondsBetweenSteps = 0.5f;

        [SerializeField]
        private List<AudioClip> clips = null;

        [SerializeField]
        private List<AudioSource> sources = null;

        #endregion

        private int lastSourceIndex = 0;
        private bool wasMoving = false;
        private float secondsSinceFootstep = 0;
        private float slowestSpeed;

        private void Start()
        {
            var controller = GetComponent<IMovementInput>();
            controller.onInput.AddListener(OnMovementInput);
            slowestSpeed = controller.Speed;
        }

        private void OnMovementInput(Vector3 velocity)
        {
            if (velocity.sqrMagnitude > 0)
            {
                float speedFactor = GetComponent<IMovementInput>().Speed / slowestSpeed;
                float proportionalSecondsBetweenSteps = secondsBetweenSteps / speedFactor;

                if (!wasMoving)
                {
                    float halfSecondsBetween = proportionalSecondsBetweenSteps / 2;
                    if (secondsSinceFootstep < halfSecondsBetween)
                        secondsSinceFootstep = halfSecondsBetween;

                    wasMoving = true;
                }

                secondsSinceFootstep += Time.deltaTime;

                if (secondsSinceFootstep >= proportionalSecondsBetweenSteps)
                {
                    secondsSinceFootstep = 0;
                    PlayFootstep();
                }
            }
            else if (wasMoving)
            {
                wasMoving = false;
            }
        }

        private void PlayFootstep()
        {
            int randomIndex = UnityEngine.Random.Range(0, clips.Count);
            AudioClip randomClip = clips[randomIndex];

            lastSourceIndex = (lastSourceIndex + 1) % sources.Count;

            sources[lastSourceIndex].PlayOneShot(randomClip);
        }

        private void OnDestroy()
        {
            GetComponent<IMovementInput>().onInput.RemoveListener(OnMovementInput);
        }
    }
    
    [Serializable]
    public class UnityEventPlayerInputFootsteps : UnityEvent<PlayerInputFootsteps> { }
}