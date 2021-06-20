using DG.Tweening;
using Horror.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Zenject;

namespace Horror
{
    public class DepthsMonster : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private Transform arms = null;

        [SerializeField]
        private float delayPerArm = 0.2f;

        [SerializeField]
        private float dragPlayerDelay = 1f;

        #endregion

        [Inject(Id = "player")]
        private PlayerInputRigidBody playerInput = null;

        //[Inject]
        //private Volume urpVolume = null;

        [Inject]
        private PostProcessVolume volume = null;

        [Inject(Id = "music")]
        private AudioSource musicSource = null;

        private void Start()
        {
            playerInput.enabled = false;
            StartCoroutine(ActivateArmsCoroutine());
            Invoke(nameof(DragPlayerDown), dragPlayerDelay);
        }

        private IEnumerator ActivateArmsCoroutine()
        {
            foreach (Transform child in arms)
            {
                yield return new WaitForSeconds(delayPerArm);
                child.gameObject.SetActive(true);
            }
        }

        private void DragPlayerDown()
        {
            playerInput.GetComponent<Animator>().Play("DraggedBelow");

            //var colorAdjustments = urpVolume.profile.components.FirstOrDefault(component => component is ColorAdjustments) as ColorAdjustments;
            //colorAdjustments.active = true;
            //DOTween.To(
            //    () => colorAdjustments.colorFilter.value,
            //    color => colorAdjustments.colorFilter.value = color,
            //    Color.black,
            //    duration: 1.1f
            //).SetDelay(0.8f);

            musicSource.DOFade(0, duration: 0.5f);

            var colorGrading = volume.profile.GetSetting<ColorGrading>();
            colorGrading.active = true;
            var tween = DOTween.To(
                () => colorGrading.colorFilter.value,
                color => colorGrading.colorFilter.value = color,
                Color.black,
                duration: 1.1f
            ).SetDelay(0.8f);

            tween.onComplete += OnPlayerDragged;
        }

        private void OnPlayerDragged()
        {
            SceneManager.LoadSceneAsync(gameObject.scene.name, LoadSceneMode.Single);
        }
    }
    
    [Serializable]
    public class UnityEventDepthsMonster : UnityEvent<DepthsMonster> { }
}