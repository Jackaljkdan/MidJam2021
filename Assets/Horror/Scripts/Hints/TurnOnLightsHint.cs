using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Hints
{
    public class TurnOnLightsHint : TriggeredAction
    {
        #region Inspector



        #endregion

        [Inject]
        private ToastText toastText = null;

        protected override void PerformTriggeredAction()
        {
            toastText.Show("I should turn on the lights");
        }
    }
    
    [Serializable]
    public class UnityEventTurnOnLightsHint : UnityEvent<TurnOnLightsHint> { }
}