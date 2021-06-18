using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Horror.Input
{
    public class InputActivate : MonoBehaviour
    {
        #region Inspector

        public GameObject target;

        #endregion

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F) && target != null)
                target.SetActive(true);
        }
    }
}