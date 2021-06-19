using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class StillnessActivator : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private bool destroyAfterActivation = true;

        #endregion

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        [Inject]
        private void Inject()
        {
            stillnessMeter.onStillnessMeasure.AddListener(OnStillnessMeasured);
        }

        private void OnDestroy()
        {
            if (stillnessMeter)
                stillnessMeter.onStillnessMeasure.RemoveListener(OnStillnessMeasured);
        }

        private void OnStillnessMeasured(float stillness)
        {
            if (Mathf.Approximately(stillness, 1))
            {
                gameObject.SetActive(true);

                if (destroyAfterActivation)
                    Destroy(this);
            }
        }
    }
}