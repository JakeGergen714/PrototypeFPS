using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.gun;
using player.move;
using UnityEngine;

namespace player
{
    public class
        Player : MonoBehaviour,
            RotationSubscriber //TODO make this have all the player components: camera, move, shoot, health (buncha hit boxes)
    {
        public float Sensitivity
        {
            get { return sensitivity; }
            set { sensitivity = value; }
        }

        [Range(0.1f, 9f)] [SerializeField] float sensitivity = 20f;

        [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
        [Range(0f, 90f)]
        [SerializeField]
        float yRotationLimit = 88f;

        Vector2 rotation = Vector2.zero;

        private CharacterController characterController;
        private Gun playerGun;

        public float jumpHeight = 30;
        public float speed = 100;
        public float sprintSpeed = 100;
        public float GRAVITY = -9.8f;

        private Quaternion subscribedRotation = new Quaternion(0, 0, 0, 0);
        private Camera playerCam;
        private bool groundedPlayer;
        private Vector3 playerVelocity;
        private Vector3 playerMovement;
        private Vector3 playerInput = Vector3.zero;
        private bool pressJump = false;
        private bool pressSprint = false;

        void Update()
        {
            look();
            Vector3 forward = playerCam.transform.forward;
            Vector3 right = playerCam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            playerInput = (forward * InputListener.getVerticalAxis()) + (right * InputListener.getHorizontalAxis());
            pressJump = InputListener.getJumpAxis() > 0;
            pressSprint = InputListener.getSprintAxis() > 0;
        }

        private void FixedUpdate()
        {
            move(playerInput, pressJump, pressSprint);
        }


        // Start is called before the first frame update
        void Start()
        {
            characterController = GameObject.FindObjectOfType<CharacterController>();
            playerCam = GameObject.FindObjectOfType<Camera>();
            playerGun = GameObject.FindObjectOfType<Gun>();

            playerGun.subscribe(this);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void look()
        {
            rotation.x += Input.GetAxis(InputKeys.MOUSE_X_AXIS) * sensitivity;
            rotation.y += Input.GetAxis(InputKeys.MOUSE_Y_AXIS) * sensitivity;
            rotation.x += subscribedRotation.x;
            rotation.y += subscribedRotation.y;
            subscribedRotation.x = 0;
            subscribedRotation.y = 0;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            transform.localRotation = xQuat * yQuat;
            //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }

        private void move(Vector3 playerInput, bool dojump, bool doSprint)
        {
            groundedPlayer = characterController.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (groundedPlayer)
            {
                Vector3 characterVel = characterController.velocity;
                Vector3 camaraForward = playerCam.transform.forward;
                characterVel.y = 0;
                camaraForward.y = 0;

                if (Vector3.Cross(characterVel, camaraForward) == Vector3.zero && doSprint)
                {
                    playerMovement = playerInput * Time.deltaTime * (speed + sprintSpeed);
                }
                else
                {
                    playerMovement = playerInput * Time.deltaTime * speed;
                }

            }

            if (playerMovement != Vector3.zero)
            {
                gameObject.transform.forward = playerCam.transform.forward;
            }

            if (dojump && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY);
            }

            playerVelocity.y += GRAVITY * Time.deltaTime;
            characterController.Move(playerMovement + playerVelocity * Time.deltaTime);
        }


        public void lookChange(Quaternion rotation)
        {
            this.subscribedRotation = rotation;
        }
    }
}