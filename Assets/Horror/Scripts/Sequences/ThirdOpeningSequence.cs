using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Sequences
{
    [RequireComponent(typeof(WakeUpSequence))]
    public class ThirdOpeningSequence : MonoBehaviour
    {
        #region Inspector



        #endregion

        [Inject]
        private ToastText toastText = null;

        private void Start()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.AddListener(OnWakeUp);
            Invoke(nameof(ShowPhewMessage), 2f);
        }

        private void ShowPhewMessage()
        {
            toastText.Show("I'm awake now... right?!");
        }

        private void OnWakeUp()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.RemoveListener(OnWakeUp);
            toastText.Show("No! Make it stop!");
        }
    }
    
    [Serializable]
    public class UnityEventThirdOpeningSequence : UnityEvent<ThirdOpeningSequence> { }
}