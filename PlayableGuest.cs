using System;
using UnityEngine;
using Parkitect.UI;

namespace PeepCam
{
    public class PlayableGuest : MonoBehaviour
    {
        public bool Active = false;

        private float _speed = 0.9f;
        private float _runSpeed = 1.7f;
        private float _jumpSpeed = 2.2f;
        private float _gravity = 7.0f;
        private Vector3 _moveDirection = Vector3.zero;
        private CharacterController _cc;
        private MouseLookAround _mla;
        private Camera _camera;
        private GameObject _origCam;

        public void Start()
        {
            _origCam = Camera.main.gameObject;

            _cc = this.gameObject.AddComponent<CharacterController>();
            _cc.radius = 0.3f;
            _cc.height = 0.75f;

            _mla = this.gameObject.AddComponent<MouseLookAround>();

            _origCam.GetComponent<CameraController>().enabled = false;
            _camera = this.gameObject.AddComponent<Camera>();
            _camera.nearClipPlane = 0.03f;
            _camera.farClipPlane = 100f;
            _camera.cullingMask = ~LayerMasks.ID_TERRAINCLIPS;
            CullingGroupManager.Instance.setTargetCamera(_camera);
            _camera.depthTextureMode = DepthTextureMode.DepthNormals;
            this.gameObject.AddComponent<AudioListener>();

            UIWorldOverlayController.Instance.gameObject.SetActive(false);
            UIScaledOverlayController.Instance.gameObject.SetActive(false);


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void InitFromGuest(Guest guest)
        {
            this.transform.position = guest.head.transform.position;
            this.transform.eulerAngles = guest.head.transform.rotation.eulerAngles;
            Active = true;
        }

        public void Update()
        {
            if (Active)
            {
                if (_cc.isGrounded && Active)
                {
                    _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    _moveDirection = transform.TransformDirection(_moveDirection);
                    if (Input.GetKey(KeyCode.LeftShift))
                        _moveDirection *= _runSpeed;
                    else
                        _moveDirection *= _speed;

                    if (Input.GetKeyUp(KeyCode.Space))
                        _moveDirection.y = _jumpSpeed;

                }
                _moveDirection.y -= _gravity * Time.deltaTime;
                _cc.Move(_moveDirection * Time.deltaTime);
            }
        }

        public void OnDestroy()
        {
            _origCam.GetComponent<CameraController>().enabled = true;

            CullingGroupManager.Instance.setTargetCamera(_origCam.GetComponent<Camera>());

            UIWorldOverlayController.Instance.gameObject.SetActive(true);
            UIScaledOverlayController.Instance.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
