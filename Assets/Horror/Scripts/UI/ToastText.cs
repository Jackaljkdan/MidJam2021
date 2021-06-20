using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Horror.UI
{
    [RequireComponent(typeof(Text))]
    public class ToastText : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private float defaultStaySeconds = 2f;

        #endregion

        private Coroutine coroutine;

        private void Start()
        {
            GetComponent<Text>().color = new Color(1, 1, 1, 0);
        }

        public Coroutine Show(string message)
        {
            return Show(message, defaultStaySeconds);
        }

        public Coroutine Show(string message, float staySeconds)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            var textUi = GetComponent<Text>();
            textUi.text = message;
            coroutine = StartCoroutine(ShowCoroutine(textUi, staySeconds));
            return coroutine;
        }

        private IEnumerator ShowCoroutine(Text textUi, float staySeconds)
        {
            DOTween.Kill(textUi);
            yield return textUi.DOFade(1, 0.25f).WaitForCompletion();
            yield return new WaitForSeconds(staySeconds);
            yield return textUi.DOFade(0, 0.25f).WaitForCompletion();
        }
    }
    
    [Serializable]
    public class UnityEventToastText : UnityEvent<ToastText> { }
}