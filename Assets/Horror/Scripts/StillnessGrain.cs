using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Horror
{
    public class StillnessGrain : MonoBehaviour
    {
        [Inject]
        private PostProcessVolume volume = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        private void Start()
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
            var grain = volume.profile.GetSetting<Grain>();
            grain.intensity.value = stillness;
        }
    }
}