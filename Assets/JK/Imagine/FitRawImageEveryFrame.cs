using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JK.Imagine
{
    [RequireComponent(typeof(RawImageAspectRatio))]
    public class FitRawImageEveryFrame : MonoBehaviour
    {
        #region Inspector



        #endregion

        private void Update()
        {
            GetComponent<RawImageAspectRatio>().Fit();
        }
    }
    
    [Serializable]
    public class UnityEventFitRawImageEveryFrame : UnityEvent<FitRawImageEveryFrame> { }
}