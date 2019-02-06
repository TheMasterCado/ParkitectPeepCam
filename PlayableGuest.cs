using System;
using UnityEngine;
using Parkitect.UI;

namespace PeepCam
{
    public class PlayableGuest : MonoBehaviour
    {
        public bool Active = false;

        private float speed = 0.9f;
        private float runSpeed = 1.7f;
        private float jumpSpeed = 2.2f;
        private float gravity = 7.0f;
        private Vector3 moveDirection = Vector3.zero;
        private CharacterController cc;
        private MouseLookAround mla;
        private Camera camera;

        public void Start()
        {
            cc = this.gameObject.AddComponent<CharacterController>();
            cc.radius = 0.3f;
            cc.height = 0.75f;

            mla = this.gameObject.AddComponent<MouseLookAround>();

            Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
            camera = this.gameObject.AddComponent<Camera>();
            camera.nearClipPlane = 0.03f;
            camera.farClipPlane = 100f;
            camera.cullingMask = Camera.main.cullingMask;
            CullingGroupManager.Instance.setTargetCamera(camera);
            camera.depthTextureMode = DepthTextureMode.DepthNormals;
            this.gameObject.AddComponent<AudioListener>();

            UIWorldOverlayController.Instance.gameObject.SetActive(false);

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
                if (cc.isGrounded && Active)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    if (Input.GetKey(KeyCode.LeftShift))
                        moveDirection *= runSpeed;
                    else
                        moveDirection *= speed;

                    if (Input.GetKeyUp(KeyCode.Space))
                        moveDirection.y = jumpSpeed;

                }
                moveDirection.y -= gravity * Time.deltaTime;
                cc.Move(moveDirection * Time.deltaTime);
            }
        }

        public void OnDestroy()
        {
            Camera.main.gameObject.GetComponent<CameraController>().enabled = true;

            CullingGroupManager.Instance.setTargetCamera(Camera.main);

            UIWorldOverlayController.Instance.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
