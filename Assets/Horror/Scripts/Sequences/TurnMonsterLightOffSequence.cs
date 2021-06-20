using Horror.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        protected override void PerformTriggeredAction()
        {
            lightSwitch.Interact(new RaycastHit());
            monsterLight.Light.GetComponent<FlickeringLight>().enabled = false;
            monsterLight.Light.intensity = 0;
            monster.SetActive(false);
        }
    }
    
    [Serializable]
    public class UnityEventTurnMonsterLightOffSequence : UnityEvent<TurnMonsterLightOffSequence> { }
}