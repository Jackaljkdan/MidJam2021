using DG.Tweening;
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
    public class GreenSymbolSequence : TriggeredAction
    {
        #region Inspector

        [SerializeField]
        private List<LightTargetBehaviour> lights = null;

        [SerializeField]
        private ForceCameraLook symbolForceLook = null;

        #endregion

        [Inject(Id = "player")]
        private PlayerInputRigidBody playerBodyInput = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        [Inject(Id = "depths_monster")]
        private DepthsMonster depthsMonster = null;

        [Inject]
        private ToastText toastText = null;

        protected override void PerformTriggeredAction()
        {
            toastText.StartCoroutine(SequenceCoroutine());
        }

        private IEnumerator SequenceCoroutine()
        {
            foreach (var light in lights)
            {
                var flicker = light.Light.GetComponent<FlickeringLight>();
                flicker.maxIntensity = 0.3f;
                flicker.minIntensity = 0.25f;
            }

            depthsMonster.restartLevel = false;
            stillnessMeter.enabled = false;

            symbolForceLook.enabled = true;

            toastText.Show("What is happening?!", staySeconds: 1);

            yield return new WaitForSeconds(1);

            symbolForceLook.enabled = false;

            stillnessMeter.enabled = true;
            stillnessMeter.increaseMultiplier = 3;

            DOTween.To(
                () => playerBodyInput.acceleration,
                val => playerBodyInput.acceleration = val,
                0,
                duration: 1.5f
            ).onComplete += () => playerBodyInput.enabled = false;

            yield return new WaitForSeconds(1f);

            toastText.Show("I can't move!", staySeconds: 1);
        }
    }
    
    [Serializable]
    public class UnityEventGreenSymbolSequence : UnityEvent<GreenSymbolSequence> { }
}