using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    public abstract class LightTargetBehaviour : MonoBehaviour, ILightTarget
    {
        #region Inspector



        #endregion

        public abstract void Toggle();
    }
}