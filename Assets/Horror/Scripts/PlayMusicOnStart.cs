using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror
{
    public class PlayMusicOnStart : MonoBehaviour
    {
        #region Inspector



        #endregion

        [Inject(Id = "music")]
        private AudioSource musicSource = null;

        private void Start()
        {
            if (!musicSource.isPlaying)
                musicSource.Play();
        }
    }
}