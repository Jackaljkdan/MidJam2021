using Horror.Interaction;
using Horror.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Horror.Hints
{
    public class RedDoorHint : StayTriggerAction
    {
        #region Inspector

        [SerializeField]
        private LightTargetBehaviour doorLight = null;

        [SerializeField]
        private DoorInteractable bathroomDoor = null;

        #endregion

        [Inject(Id = "player.camera")]
        private Transform cameraTransform = null;

        [Inject]
        private ToastText toastText = null;

        protected override bool CanPerformAction()
        {
            Vector3 directionToDoor = (bathroomDoor.MovingPiece.transform.position - cameraTransform.position).normalized;
            float dot = Vector3.Dot(directionToDoor, cameraTransform.forward);

            return doorLight.Light.intensity > 0 && dot >= 0.6f;
        }

        protected override void PerformTriggeredAction()
        {
            bathroomDoor.MovingPiece.GetComponent<ForceCameraLook>().enabled = true;
            toastText.StartCoroutine(StopForcingLookat());

            toastText.Show("What?! The door... It's not supposed to be red!");
        }

        private IEnumerator StopForcingLookat()
        {
            yield return new WaitForSeconds(1);
            bathroomDoor.MovingPiece.GetComponent<ForceCameraLook>().enabled = false;
        }
    }
    
    [Serializable]
    public class UnityEventRedDoorHint : UnityEvent<RedDoorHint> { }
}