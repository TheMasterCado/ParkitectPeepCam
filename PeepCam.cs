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
                var guest = GuestUnderMouse();

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

        private static Guest GuestUnderMouse()
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

            Camera.main.GetComponent<CameraController>().enabled = false;

            _playableGuest = new GameObject();
            _playableGuest.AddComponent<PlayableGuest>().InitFromGuest(guest);

            _playableGuest.AddComponent<Camera>().nearClipPlane = 0.01f;
            _playableGuest.GetComponent<Camera>().farClipPlane = 100f;
            _playableGuest.AddComponent<AudioListener>();

            _isInGuest = true;
        }

        private void LeaveGuest()
        {
            if (!_isInGuest)
                return;

            Camera.main.GetComponent<CameraController>().enabled = true;

            Destroy(_camera);
            Destroy(_imaginaryGuest);

            _isInGuest = false;
        }
    }
}
