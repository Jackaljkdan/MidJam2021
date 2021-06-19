using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Interaction
{
    public interface ILightTarget
    {
        Light Light { get; }

        void Toggle();
    }
    
    [Serializable]
    public class UnityEventILightTarget : UnityEvent<ILightTarget> { }
}