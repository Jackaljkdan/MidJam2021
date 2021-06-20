using Horror.Input;
using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Sequences
{
    public class OpeningSequence : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private Transform playerAnchor = null;

        [SerializeField]
        private LightSwitchInteractable bajour = null;

        [SerializeField]
        private bool playInEditor = true;

        #endregion

        [Inject(Id = "player")]
        private PlayerInputRigidBody bodyInput = null;

        [Inject(Id = "player.camera")]
        private PlayerInputRotation rotationInput = null;

        [Inject(Id = "player")]
        private Animator playerAnimator = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        [Inject]
        private ToastText toastText = null;

        private void Start()
        {
            if (Application.isEditor && !playInEditor)
                return;

            bodyInput.enabled = false;
            rotationInput.enabled = false;
            stillnessMeter.enabled = false;

            StartCoroutine(SequenceCoroutine());
        }

        private IEnumerator SequenceCoroutine()
        {
            bodyInput.transform.position = playerAnchor.position;
            bodyInput.transform.rotation = playerAnchor.rotation;

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

            toastText.Show("I don't feel good about standing still...");
        }
    }
    
    [Serializable]
    public class UnityEventOpeningSequence : UnityEvent<OpeningSequence> { }
}