using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Sequences
{
    public class SecondOpeningSequence : MonoBehaviour
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
            toastText.Show("It was just a nightmare");
        }

        private void OnWakeUp()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.RemoveListener(OnWakeUp);
            toastText.Show("Oh no...");
        }
    }
    
    [Serializable]
    public class UnityEventSecondOpeningSequence : UnityEvent<SecondOpeningSequence> { }
}