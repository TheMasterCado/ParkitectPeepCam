using System;
using UnityEngine;

namespace PeepCam
{
    public class PlayableGuest : MonoBehaviour
    {
        public bool Active = false;

        private float speed = 7.0f;
        private float jumpSpeed = 6.0f;
        private float gravity = 20.0f;
        private Vector3 moveDirection = Vector3.zero;
        private CharacterController cc;
        private MouseLookAround mla;

        public void Start()
        {
            cc = this.gameObject.AddComponent<CharacterController>();
            cc.radius = 0.1f;
            cc.height = 0.3f;
            mla = this.gameObject.AddComponent<MouseLookAround>();
        }

        public void InitFromGuest(Guest guest)
        {
            this.transform.position = guest.transform.position;
            this.transform.eulerAngles = guest.transform.rotation.eulerAngles;
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
                    moveDirection *= speed;

                    if (Input.GetButton("Jump"))
                        moveDirection.y = jumpSpeed;

                }
                moveDirection.y -= gravity * Time.deltaTime;
                cc.Move(moveDirection * Time.deltaTime);
            }
        }
    }
}
