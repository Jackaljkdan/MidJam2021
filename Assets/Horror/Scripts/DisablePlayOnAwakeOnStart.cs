using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror
{
    [RequireComponent(typeof(AudioSource))]
    public class DisablePlayOnAwakeOnStart : MonoBehaviour
    {
        #region Inspector

        

        #endregion

        private void OnEnable()
        {
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return null;
            GetComponent<AudioSource>().playOnAwake = false;
            Destroy(this);
        }
    }
    
    [Serializable]
    public class UnityEventDisablePlayOnAwakeOnStart : UnityEvent<DisablePlayOnAwakeOnStart> { }
}