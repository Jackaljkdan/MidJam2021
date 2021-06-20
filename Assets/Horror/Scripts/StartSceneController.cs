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
            Cursor.lockState = CursorLockMode.Locked;
            group.DOFade(0, 2.2f);
            Invoke(nameof(LoadGameScene), 2f);
        }

        private void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
        }
    }
    
    [Serializable]
    public class UnityEventStartSceneController : UnityEvent<StartSceneController> { }
}