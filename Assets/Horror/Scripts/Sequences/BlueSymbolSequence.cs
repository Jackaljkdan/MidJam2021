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
using UnityEngine.SceneManagement;
using Zenject;

namespace Horror.Sequences
{
    public class BlueSymbolSequence : TriggeredAction
    {
        #region Inspector

        [SerializeField]
        private List<LightTargetBehaviour> lights = null;

        [SerializeField]
        private ForceCameraLook symbolForceLook = null;

        [SerializeField]
        private DoorInteractable blueDoor = null;

        [SerializeField]
        private GameObject monster = null;

        #endregion

        [Inject(Id = "player")]
        private Transform playerTransform = null;

        [Inject(Id = "player")]
        private PlayerInputRigidBody playerBodyInput = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        [Inject(Id = "music")]
        private AudioSource musicSource = null;

        [Inject(Id = "jumpscare.4s")]
        private AudioSource jumpscareSource = null;

        [Inject]
        private PostProcessVolume volume = null;

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

            stillnessMeter.enabled = false;
            symbolForceLook.enabled = true;
            blueDoor.ForceClose(disableCollider: true);
            blueDoor.IsLocked = true;

            yield return new WaitForSeconds(1);

            symbolForceLook.enabled = false;

            yield return new WaitForSeconds(4);

            musicSource.DOFade(0, duration: 0.5f);

            foreach (var light in lights)
            {
                light.Light.GetComponent<FlickeringLight>().StopFlicker();
                light.Light.intensity = 0;
            }

            yield return new WaitForSeconds(1f);

            monster.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(5f);

            foreach (var light in lights)
            {
                var flicker = light.Light.GetComponent<FlickeringLight>();
                flicker.minIntensity = 0;
                flicker.StartFlicker();

                light.Light.intensity = 0.3f;
            }

            playerBodyInput.enabled = false;
            monster.GetComponent<ForceCameraLook>().enabled = true;
            monster.GetComponent<LookAtPlayer>().enabled = true;
            monster.GetComponent<Animator>().Play("MonsterAttackStance");
            jumpscareSource.Play();

            float moveToPlayerSeconds = 1;
            monster.transform.DOMove(playerTransform.position, duration: moveToPlayerSeconds);

            yield return new WaitForSeconds(moveToPlayerSeconds - 0.8f);

            var colorGrading = volume.profile.GetSetting<ColorGrading>();
            colorGrading.active = true;
            var tween = DOTween.To(
                () => colorGrading.colorFilter.value,
                color => colorGrading.colorFilter.value = color,
                Color.black,
                duration: 0.2f
            );

            yield return tween.WaitForCompletion();

            // wait jumpscare sound to finish
            yield return new WaitForSeconds(3);

            SceneManager.LoadSceneAsync("GamePart4", LoadSceneMode.Single);
        }
    }
    
    [Serializable]
    public class UnityEventBlueSymbolSequence : UnityEvent<BlueSymbolSequence> { }
}