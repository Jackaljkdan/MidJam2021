using Horror.Input;
using Horror.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class OpeningSequence : MonoBehaviour
    {
        #region Inspector

        public LightSwitchInteractable bajour;

        #endregion

        [Inject(Id = "player")]
        private PlayerInputRigidBody bodyInput = null;

        [Inject(Id = "player.camera")]
        private PlayerInputRotation rotationInput = null;

        [Inject(Id = "player")]
        private Animator playerAnimator = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        private void Start()
        {
            bodyInput.enabled = false;
            rotationInput.enabled = false;
            stillnessMeter.enabled = false;

            StartCoroutine(SequenceCoroutine());
        }

        private IEnumerator SequenceCoroutine()
        {
            playerAnimator.Play("WakeUp");
            playerAnimator.speed = 0;

            yield return new WaitForSeconds(1);
            bajour.Interact(new RaycastHit());

            // TODO: blink?

            yield return new WaitForSeconds(1);
            playerAnimator.speed = 1;

            yield return new WaitForSeconds(3);
            bodyInput.enabled = true;
            rotationInput.enabled = true;
            stillnessMeter.enabled = true;
        }
    }
    
    [Serializable]
    public class UnityEventOpeningSequence : UnityEvent<OpeningSequence> { }
}