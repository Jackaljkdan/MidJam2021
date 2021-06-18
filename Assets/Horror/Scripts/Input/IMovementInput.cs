using JK.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Input
{
    public interface IMovementInput
    {
        float Speed { get; set; }
        UnityEventVector3 onInput { get; }
    }

    [Serializable]
    public class UnityEventIMovementInput : UnityEvent<IMovementInput> { }
}