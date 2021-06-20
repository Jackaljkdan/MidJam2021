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
    public class FirstOpeningSequence : MonoBehaviour
    {
        #region Inspector



        #endregion

        [Inject]
        private ToastText toastText = null;

        private void Start()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.AddListener(OnWakeUp);
        }

        private void OnWakeUp()
        {
            GetComponent<WakeUpSequence>().onSequenceEnd.RemoveListener(OnWakeUp);
            toastText.Show("I don't feel good about standing still...");
        }
    }
    
    [Serializable]
    public class UnityEventFirstOpeningSequence : UnityEvent<FirstOpeningSequence> { }
}