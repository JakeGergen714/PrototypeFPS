using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using DefaultNamespace.gun.orientation;
using gun.viewbob;
using player.move;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Purchasing;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

        [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")] [Range(0f, 90f)] [SerializeField]
        float yRotationLimit = 88f;

        Vector2 rotation = Vector2.zero;

        private CharacterController characterController;
        private RecoilController recoilController;

        private HealthController healthController;

        public float jumpHeight = .5f;
        public float speed = 15;
        public float sprintSpeed = 3;
        public float GRAVITY = -20.8f;
        public float MAX_SPEED = 0.25f;
        public float DRAG_FACTOR = 0.1f;

        public float smoothTime = 0f;

        private Vector3 currentVelocity;

        private Quaternion subscribedRotation = new Quaternion(0, 0, 0, 0);
        private Camera playerCam;
        private bool groundedPlayer;
        private Vector3 playerVerticalVelocity;
        private Vector3 playerHorizontalVelocity;
        private Vector3 playerMovement;
        private Vector3 playerInput = Vector3.zero;
        private bool pressJump = false;
        private bool pressSprint = false;

        public float pos_floor = 0f;

        public float floor_threshold = 0f; //the point at which the floor takes effect. I want the ability to counter strafe without immedaitly going in other direction

        public bool debug = false;

        public bool isMe = false;

        private OrientationController orientationController;

        private ViewBobController viewBobController;
        private float bounceFactor = 0.0f;
        public static int BOUNCE_PATTERN_SIZE = 0;
        public Vector2 bouncePattern = Vector2.zero;
        private int bounceDirection = 1;

        public int MAX_JUMP_COOLDOWN = 100;
        private int currentJumpCoolDown = 0;

        private float lastHorizontalInput;
        private float lastVerticalInput;

        private float verticalAxisInput;
        private float horizonalAxisInput;

        private bool isJumping = false;
        private InputManager inputManager;

        private AudioListener audioListener;

        [SerializeField] private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();

        [SerializeField] private NetworkVariable<Quaternion> networkRotationDirection = new NetworkVariable<Quaternion>();

        private Vector3 forward;
        private Vector3 right;

        private Vector3 targetVel;
        private Quaternion lookInput;

        private float timer;
        private int currentTick;

        private const float SERVER_TICK_RATE = 30f;
        private const float MIN_TIME_BETWEEN_TICKS = 1f / SERVER_TICK_RATE;
        private const int BUFFER_SIZE = 1024;
        
        private PlayerInputPacket[] playerInputs = new PlayerInputPacket[BUFFER_SIZE];
        private PlayerStatePacket[] playerStates = new PlayerStatePacket[BUFFER_SIZE];

        private Queue<PlayerInputPacket> clientInputQueue = new Queue<PlayerInputPacket>(); //server queues input recieved from client
        void Start()
        {
            recoilController = GetComponentInChildren<RecoilController>();
            recoilController.subscribe(this);

            Debug.Log("I am the owner of this Player component.");
            playerCam = GetComponentInChildren<Camera>();
            characterController = GetComponentInChildren<CharacterController>();
            orientationController = GetComponentInChildren<OrientationController>();
            viewBobController = GetComponentInChildren<ViewBobController>();

            inputManager = GetComponent<InputManager>();
            inputManager.addAxis("Horizontal", "d", "a");
            inputManager.addAxis("Vertical", "w", "s");


            healthController = gameObject.GetComponentInChildren<HealthController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        void Update()
        {
            timer += Time.deltaTime;
            
            while (timer >= MIN_TIME_BETWEEN_TICKS)
            {
                timer -= MIN_TIME_BETWEEN_TICKS;
                HandleTick();
                currentTick++;
            }
            
            playerInput = getAxisInput();
            pressJump = InputListener.getJumpAxis() > 0;
            pressSprint = InputListener.getSprintAxis() > 0;
            transform.localRotation = look();

            doWeaponBounce();
        }

        private Vector3 getAxisInput()
        {
            Vector3 forward = playerCam.transform.forward;
            Vector3 right = playerCam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            verticalAxisInput = inputManager.getValue("Vertical");
            horizonalAxisInput = inputManager.getValue("Horizontal");

            if (verticalAxisInput < .05 & verticalAxisInput > 0)
            {
                verticalAxisInput = .05f;
            }

            if (verticalAxisInput > -.1 & verticalAxisInput < 0)
            {
                verticalAxisInput = -.05f;
            }

            if (horizonalAxisInput < .05 & horizonalAxisInput > 0)
            {
                horizonalAxisInput = .05f;
            }

            if (horizonalAxisInput > -.05 & horizonalAxisInput < 0)
            {
                horizonalAxisInput = -.05f;
            }

            verticalAxisInput *= 100;
            if (verticalAxisInput > 0)
            {
                verticalAxisInput *= verticalAxisInput / 4;
            }
            else
            {
                verticalAxisInput *= -verticalAxisInput / 4;
            }

            verticalAxisInput /= 100;


            if (verticalAxisInput > 1)
            {
                verticalAxisInput = 1;
            }

            if (verticalAxisInput < -1)
            {
                verticalAxisInput = -1;
            }

            if (debug)
            {
                Debug.Log("vert axis:" + verticalAxisInput);
            }

            horizonalAxisInput *= 100;
            if (horizonalAxisInput > 0)
            {
                horizonalAxisInput *= horizonalAxisInput / 4;
            }
            else
            {
                horizonalAxisInput *= -horizonalAxisInput / 4;
            }

            horizonalAxisInput /= 100;

            if (horizonalAxisInput > 1)
            {
                horizonalAxisInput = 1;
            }

            if (horizonalAxisInput < -1)
            {
                horizonalAxisInput = -1;
            }

            if (debug)
            {
                Debug.Log("hort axis:" + horizonalAxisInput);
            }

            if (InputListener.getSprintAxis() != 0)
            {
                if (horizonalAxisInput < 0)
                {
                    horizonalAxisInput = -1;
                }

                if (horizonalAxisInput > 0)
                {
                    horizonalAxisInput = 1;
                }

                if (verticalAxisInput < 0)
                {
                    verticalAxisInput = -1;
                }

                if (verticalAxisInput > 0)
                {
                    verticalAxisInput = 1;
                }
            }

            return (forward * verticalAxisInput) + (right * horizonalAxisInput);
        }

        private void FixedUpdate()
        {
            if (currentJumpCoolDown > 0)
            {
                currentJumpCoolDown--;
            }

            playerInputs[0] = new PlayerInputPacket(playerInput, pressJump, pressSprint,0 );//TODO make this a circular input buffer using fixed tickrate as index
            move(playerInput, pressJump, pressSprint);

            playerStates[0] = new PlayerStatePacket(transform.position, 0);
        }

        private Quaternion look()
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

            return xQuat * yQuat;
            //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }

        private void move(Vector3 playerInput, bool dojump, bool doSprint)
        {
            groundedPlayer = characterController.isGrounded;

            if (!groundedPlayer)
            {
                inputManager.doPause();
            }
            else
            {
                inputManager.doUnpause();
            }

            if (groundedPlayer && playerVerticalVelocity.y < 0)
            {
                playerVerticalVelocity.y = 0f;
            }

            if (groundedPlayer)
            {
                Vector3 characterVel = characterController.velocity;
                Vector3 camaraForward = playerCam.transform.forward;
                characterVel.y = 0;
                camaraForward.y = 0;


                if (doSprint) //if character val and camara are in the exact same direction. i.e parralel i.e cross vector = 0 then sprint
                {
                    playerMovement = (playerInput * Time.deltaTime * (speed + sprintSpeed));
                }
                else
                {
                    playerMovement += (playerInput * Time.deltaTime * speed);
                    Vector2 magCheck = new Vector2(playerMovement.x, playerMovement.z);
                    magCheck = Vector2.ClampMagnitude(magCheck, MAX_SPEED);
                    playerMovement.x = magCheck.x;
                    playerMovement.z = magCheck.y;
                    playerMovement.x -= DRAG_FACTOR * playerMovement.x;
                    playerMovement.z -= DRAG_FACTOR * playerMovement.z;
                }
            }

            if (dojump && groundedPlayer && currentJumpCoolDown == 0)
            {
                playerVerticalVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY);
                currentJumpCoolDown = MAX_JUMP_COOLDOWN;
                isJumping = true;
            }

            playerVerticalVelocity.y += GRAVITY * Time.deltaTime;

            Vector3 targetVelocity = playerMovement + playerVerticalVelocity * Time.deltaTime;
            
            characterController.Move(targetVelocity);
        }

        private void doMove(Vector3 vel)
        {
            characterController.Move(vel);
        }

        private void doWeaponBounce()
        {
            bounceFactor = characterController.velocity.magnitude / 11.25f;

            if (bounceFactor > 0)
            {
                Vector2 bouncePattern = this.bouncePattern * bounceDirection;
                bouncePattern.x *= bounceFactor;
                bouncePattern.y *= bounceFactor;

                bounceDirection = viewBobController.addViewBob(bouncePattern) == true ? bounceDirection *= -1 : bounceDirection;
            }
        }

        public Vector3 applyDrag(Vector3 vector, float dragDecimal)
        {
            if (dragDecimal > 1)
            {
                throw new ArgumentException("Drag must be less than one.");
            }

            return vector * dragDecimal;
        }


        public void lookChange(Quaternion rotation)
        {
            this.subscribedRotation = rotation;
        }

        public void spawnPlayer()
        {
            Instantiate(this.gameObject);
        }

        public void despawnPlayer()
        {
            Destroy(this.gameObject);
        }

        public HealthController getHealthController()
        {
            return this.healthController;
        }
    }
}