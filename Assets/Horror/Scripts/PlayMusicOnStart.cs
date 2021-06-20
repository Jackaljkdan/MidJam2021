using DG.Tweening;
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

        [Inject(Id = "music.volume")]
        private float musicVolume = 1;


        private void Start()
        {
            if (!musicSource.isPlaying)
                musicSource.Play();

            musicSource.DOFade(musicVolume, duration: 0.5f);
        }
    }
}