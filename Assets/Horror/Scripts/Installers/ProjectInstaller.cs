using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        #region Inspector

        [SerializeField]
        private AudioSource musicSource = null;

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(musicSource.volume).WithId("music.volume").AsSingle();
        }
    }
}