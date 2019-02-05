using System;
using System.Collections.Generic;
using Parkitect.UI;
using UnityEngine;

namespace PeepCam
{
    public class PeepCam : MonoBehaviour
    {
        public static PeepCam Instance;

        private GameObject _camera;
        private PlayableGuest _playableGuest;
        private bool _isActive;

        private void Update()
        {
            if (!_isActive && InputManager.getKeyUp("TheMasterCado@PeepCam/enterPeepCam"))
            {
                var guest = GuestUnderMouse();

                if (guest != null)
                {
                    EnterGuest(guest);
                }
            }
            else if (_isActive && (InputManager.getKeyUp("TheMasterCado@PeepCam/enterPeepCam") || Input.GetKeyUp(KeyCode.Escape)))
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
            if (_isActive)
                return;

            Camera.main.GetComponent<CameraController>().enabled = false;

            _playableGuest = new PlayableGuest(guest);

            _camera = new GameObject();
            _camera.AddComponent<Camera>().nearClipPlane = 0.01f;
            _camera.GetComponent<Camera>().farClipPlane = 100f;
            _camera.AddComponent<AudioListener>();
            _camera.transform.parent = _playableGuest.Guest.head.transform;
            _camera.transform.localPosition = new Vector3(-0.09f, -0.13f, 0);
            _camera.transform.localRotation = Quaternion.Euler(90, 0, 90);

            _isActive = true;

        }

        private void LeaveGuest()
        {
            if (!_isActive)
                return;

            Camera.main.GetComponent<CameraController>().enabled = true;

            Destroy(_camera);
            Destroy(_imaginaryGuest);

            _isActive = false;
        }
    }
}
