using DG.Tweening;
using Horror.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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

        [Inject]
        private Volume volume = null;

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
            var colorAdjustments = volume.profile.components.FirstOrDefault(component => component is ColorAdjustments) as ColorAdjustments;
            colorAdjustments.active = true;
            DOTween.To(
                () => colorAdjustments.colorFilter.value,
                color => colorAdjustments.colorFilter.value = color,
                Color.black,
                duration: 1.1f
            ).SetDelay(0.8f);
        }
    }
    
    [Serializable]
    public class UnityEventDepthsMonster : UnityEvent<DepthsMonster> { }
}