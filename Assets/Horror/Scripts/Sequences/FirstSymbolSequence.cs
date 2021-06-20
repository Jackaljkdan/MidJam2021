using Horror.Input;
using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Sequences
{
    public class FirstSymbolSequence : StayTriggerAction
    {
        #region Inspector

        [SerializeField]
        private Transform symbol = null;

        [SerializeField]
        private LightTargetBehaviour bathroomLight = null;

        [SerializeField]
        private LightSwitchInteractable bathroomSwitch = null;

        [SerializeField]
        private DoorInteractable bathroomDoor = null;

        [Inject(Id = "player")]
        private StillnessMeter stillnessMeter = null;

        [SerializeField]
        private GameObject monster = null;

        #endregion

        [Inject(Id = "player")]
        private PlayerInputRigidBody bodyInput = null;

        [Inject]
        private ToastText toastText = null;

        private void Start()
        {
            monster.SetActive(false);
        }

        protected override bool CanPerformAction()
        {
            return bathroomLight.Light.intensity > 0;
        }

        protected override void PerformTriggeredAction()
        {
            bodyInput.StartCoroutine(SequenceCoroutine());
        }

        private IEnumerator SequenceCoroutine()
        {
            stillnessMeter.enabled = false;

            // Phase: look at symbol and close door
            Debug.Log("Phase: look at symbol and close door");

            bathroomLight.enabled = false;
            symbol.GetComponent<ForceCameraLook>().enabled = true;
            toastText.Show("What is that?!");
            
            yield return new WaitForSeconds(0.5f);

            bathroomDoor.ForceClose(disableCollider: true);
            bathroomDoor.IsLocked = true;

            yield return new WaitForSeconds(1.5f);

            // Phase: look at closed door
            Debug.Log("Phase: look at closed door");

            symbol.GetComponent<ForceCameraLook>().enabled = false;
            bathroomDoor.MovingPiece.GetComponent<ForceCameraLook>().enabled = true;

            yield return new WaitForSeconds(1.5f);

            // Phase: stop force look and flicker light
            Debug.Log("Phase: stop force look and flicker light");

            // TODO: turn music volume down
            bathroomDoor.MovingPiece.GetComponent<ForceCameraLook>().enabled = false;
            bathroomLight.Light.GetComponent<FlickeringLight>().enabled = true;

            yield return new WaitForSeconds(5f);

            // Phase: darkness and monster moan
            Debug.Log("Phase: darkness and monster moan");

            bathroomLight.Light.GetComponent<FlickeringLight>().enabled = false;

            bathroomLight.enabled = true;
            bathroomSwitch.Interact(new RaycastHit());
            bathroomLight.enabled = false;
            bathroomLight.Light.intensity = 0;

            yield return new WaitForSeconds(4f);

            monster.SetActive(true);

            yield return new WaitForSeconds(5f);

            // Phase: lights on, nothing weird
            Debug.Log("Phase: lights on, nothing weird");

            monster.SetActive(false);

            bathroomLight.enabled = true;
            bathroomSwitch.Interact(new RaycastHit());
            bathroomLight.enabled = false;

            yield return new WaitForSeconds(3);

            // Phase: turn lights off again, silence
            Debug.Log("Phase: turn lights off again, silence");

            bathroomLight.enabled = true;
            bathroomSwitch.Interact(new RaycastHit());
            bathroomLight.enabled = false;

            yield return new WaitForSeconds(3);

            // Phase: turn lights on with flickering and look at monster
            Debug.Log("Phase: turn lights on with flickering and look at monster");

            bathroomLight.enabled = true;
            bathroomSwitch.Interact(new RaycastHit());
            bathroomLight.enabled = false;
            bathroomLight.Light.GetComponent<FlickeringLight>().enabled = true;

            monster.SetActive(true);
            monster.GetComponent<ForceCameraLook>().enabled = true;

            stillnessMeter.ForceMeasure(0.9f);

            yield return new WaitForSeconds(0.5f);

            // TODO: play jumpscare sound

            yield return new WaitForSeconds(3);


            // TODO: turn music volume back up
        }

    }
    
    [Serializable]
    public class UnityEventFirstSymbolSequence : UnityEvent<FirstSymbolSequence> { }
}