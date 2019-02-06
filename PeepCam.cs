using System;
using System.Collections.Generic;
using Parkitect.UI;
using UnityEngine;

namespace PeepCam
{
    public class PeepCam : MonoBehaviour
    {
        public static PeepCam Instance;

        private GameObject _playableGuest;
        private bool _isInGuest = false;

        private void Update()
        {
            if (!_isInGuest && InputManager.getKeyUp("TheMasterCado@PeepCam/enterPeepCam"))
            {
                Guest guest = GuestUnderMouse();

                if (guest != null)
                {
                    EnterGuest(guest);
                }
            }
            else if (_isInGuest && (InputManager.getKeyUp("TheMasterCado@PeepCam/enterPeepCam") || Input.GetKeyUp(KeyCode.Escape)))
            {
                LeaveGuest();
            }
        }

        private void OnDestroy()
        {
            LeaveGuest();
        }

        private Guest GuestUnderMouse()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;

            var obj = Collisions.Instance.checkSelectables(ray, out distance);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, LayerMasks.MOUSECOLLIDERS))
                obj = hit.collider.gameObject;
            return obj != null ? obj.GetComponentInParent<Guest>() : null;
        }

        private void EnterGuest(Guest guest)
        {
            if (_isInGuest)
                return;

            _playableGuest = new GameObject();
            _playableGuest.AddComponent<PlayableGuest>().InitFromGuest(guest);

            _isInGuest = true;
        }

        private void LeaveGuest()
        {
            if (!_isInGuest)
                return;

            Destroy(_playableGuest);

            _isInGuest = false;
        }
    }
}
