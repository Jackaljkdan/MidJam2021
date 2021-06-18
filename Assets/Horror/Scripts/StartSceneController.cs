using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Horror
{
    public class StartSceneController : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private CanvasGroup group = null;

        #endregion

        private void Start()
        {
            group.alpha = 0;
            group.DOFade(1, 0.25f);
        }

        public void OnStartClicked()
        {
            group.DOFade(0, 1f);
            Invoke(nameof(LoadGameScene), 0.75f);
        }

        private void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
        }
    }
    
    [Serializable]
    public class UnityEventStartSceneController : UnityEvent<StartSceneController> { }
}