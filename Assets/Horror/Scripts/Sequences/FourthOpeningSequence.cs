using DG.Tweening;
using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

namespace Horror.Sequences
{
    public class FourthOpeningSequence : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private Transform monster = null;

        [SerializeField]
        private List<LightTargetBehaviour> lights = null;

        #endregion

        [Inject(Id = "player.camera")]
        private Transform playerCameraTransform = null;

        [Inject(Id = "music")]
        private AudioSource musicSource = null;

        [Inject]
        private ToastText toastText = null;

        private bool lastSequenceStarted = false;

        private void Start()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.AddListener(OnWakeUp);
            Invoke(nameof(ShowPhewMessage), 2f);
        }

        private void ShowPhewMessage()
        {
            toastText.Show("Please wake me up!");
        }

        private void OnWakeUp()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.RemoveListener(OnWakeUp);
            toastText.Show("Oh no... is there something behind me?");
        }

        private void Update()
        {
            if (lastSequenceStarted)
                return;

            Vector3 directionToMonster = (monster.position - playerCameraTransform.position).normalized;
            float dot = Vector3.Dot(directionToMonster, playerCameraTransform.forward);

            if (dot >= 0.6)
            {
                lastSequenceStarted = true;
                StartCoroutine(LastSequenceCoroutine());
            }
        }

        private IEnumerator LastSequenceCoroutine()
        {
            monster.GetComponent<ForceCameraLook>().enabled = true;

            yield return new WaitForSeconds(2);

            foreach (var light in lights)
            {
                light.Light.GetComponent<FlickeringLight>().StopFlicker();
                light.Light.intensity = 0;
            }

            musicSource.DOFade(0, duration: 0.5f);

            yield return new WaitForSeconds(2);

            monster.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(6);

            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Single);
        }
    }
    
    [Serializable]
    public class UnityEventFourthOpeningSequence : UnityEvent<FourthOpeningSequence> { }
}