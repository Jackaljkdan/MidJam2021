using DG.Tweening;
using Horror.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Sequences
{
    public class TurnMonsterLightOffSequence : TriggeredAction
    {
        #region Inspector

        [SerializeField]
        private LightTargetBehaviour monsterLight = null;

        [SerializeField]
        private LightSwitchInteractable lightSwitch = null;

        [SerializeField]
        private GameObject monster = null;

        #endregion

        [Inject(Id = "music")]
        private AudioSource musicSource = null;

        [Inject(Id = "music.volume")]
        private float musicVolume = 1;

        protected override void PerformTriggeredAction()
        {
            lightSwitch.Interact(new RaycastHit());
            monsterLight.Light.GetComponent<FlickeringLight>().enabled = false;
            monsterLight.Light.intensity = 0;
            monster.SetActive(false);

            musicSource.DOFade(0, duration: 0.5f);
            musicSource.DOFade(musicVolume, duration: 0.5f).SetDelay(8);
        }
    }
    
    [Serializable]
    public class UnityEventTurnMonsterLightOffSequence : UnityEvent<TurnMonsterLightOffSequence> { }
}