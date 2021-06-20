using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    public class Levitation : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private AnimationCurve curve = null;

        [SerializeField]
        private float loopSeconds = 5f;

        [SerializeField]
        private float offset = 1;

        #endregion

        private void Start()
        {
            Vector3 top = transform.position;
            top.y += offset/2;

            Vector3 bottom = top;
            bottom.y -= offset;

            var tween = transform.DOMove(top, loopSeconds);
            tween.startValue = bottom;
            tween.SetLoops(-1);
            tween.SetEase(curve);
        }
    }
    
    [Serializable]
    public class UnityEventLevitation : UnityEvent<Levitation> { }
}