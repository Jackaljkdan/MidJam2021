using DG.Tweening;
using Horror.Input;
using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Horror.Sequences
{
    public class WakeUpSequence : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float blacknessSeconds = 1f;

        [SerializeField]
        private Transform playerAnchor = null;

        [SerializeField]
        private LightSwitchInteractable bajour = null;

        [SerializeField]
        private bool playInEditor = true;

        public UnityEvent onBlacknessEnd = new UnityEvent();

        public UnityEvent onSequenceEnd = new UnityEvent();

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
        private PostProcessVolume volume = null;

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

            yield return new WaitForSeconds(blacknessSeconds);

            bajour.Interact(new RaycastHit());
            onBlacknessEnd.Invoke();

            yield return new WaitForSeconds(0.25f);
            yield return StartCoroutine(BlinkCoroutine(0.25f));
            yield return new WaitForSeconds(0.25f);
            yield return StartCoroutine(BlinkCoroutine(0.25f));

            playerAnimator.speed = 1;

            yield return new WaitForSeconds(3);
            bodyInput.enabled = true;
            rotationInput.enabled = true;
            stillnessMeter.enabled = true;

            onSequenceEnd.Invoke();
        }

        private IEnumerator BlinkCoroutine(float seconds)
        {
            var colorGrading = volume.profile.GetSetting<ColorGrading>();
            colorGrading.active = true;

            var tween = DOTween.To(
                () => colorGrading.colorFilter.value,
                color => colorGrading.colorFilter.value = color,
                Color.black,
                duration: seconds / 2
            );

            yield return tween.WaitForCompletion();

            tween = DOTween.To(
                () => colorGrading.colorFilter.value,
                color => colorGrading.colorFilter.value = color,
                Color.white,
                duration: seconds / 2
            );

            yield return tween.WaitForCompletion();

            colorGrading.active = false;
        }
    }
    
    [Serializable]
    public class UnityEventOpeningSequence : UnityEvent<WakeUpSequence> { }
}