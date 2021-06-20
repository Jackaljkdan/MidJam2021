using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Horror.Installers
{
    public class StuffInstaller : MonoInstaller
    {
        #region Inspector fields

        

        #endregion

        public override void InstallBindings()
        {
            Container.BindFactory<ForceCameraLook, ForceCameraLook.Factory>().AsSingle();
        }
    }
}