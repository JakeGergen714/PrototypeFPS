using System;
using DefaultNamespace;
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
        private RecoilController recoilController;

        private HealthController healthController; 

        public float jumpHeight = 30;
        public float speed = 100;
        public float sprintSpeed = 100;
        public float GRAVITY = -9.8f;
        public float MAX_SPEED = 10f;
        public float DRAG_FACTOR = .90f;

        private int continuosDirectionBonus = 1;
        private int MAX_CONTINUOS_DIRECTION_BONUS = 50;

        public float smoothTime = .1f;

        private Vector3 currentVelocity;

        private Quaternion subscribedRotation = new Quaternion(0, 0, 0, 0);
        private Camera playerCam;
        private bool groundedPlayer;
        private Vector3 playerVelocity;
        private Vector3 playerMovement;
        private Vector3 playerInput = Vector3.zero;
        private bool pressJump = false;
        private bool pressSprint = false;

        public float pos_floor = .8f;
        
        public float floor_threshold = .2f; //the point at which the floor takes effect. I want the ability to counter strafe without immedaitly going in other direction

        public bool isMe = false;

        void Start()
        {
            if (isMe)
            {
                recoilController = gameObject.GetComponentInChildren<RecoilController>();
                recoilController.subscribe(this);
                playerCam = gameObject.GetComponentInChildren<Camera>();
                characterController =gameObject.GetComponentInChildren<CharacterController>();
            }
           
            
            healthController = gameObject.GetComponentInChildren<HealthController>();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        
        void Update()
        {
            if (isMe)
            {
                look();
                Vector3 forward = playerCam.transform.forward;
                Vector3 right = playerCam.transform.right;
                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();

                float verticalAxisInput = InputListener.getVerticalAxis();
                float horizonalAxisInput = InputListener.getHorizontalAxis();

                if (verticalAxisInput < pos_floor & verticalAxisInput > 0 & verticalAxisInput > floor_threshold)
                {
                    verticalAxisInput = pos_floor;
                }
            
                if (verticalAxisInput > -pos_floor & verticalAxisInput < 0 & verticalAxisInput < -floor_threshold)
                {
                    verticalAxisInput = -pos_floor;
                }
            
                if (horizonalAxisInput < pos_floor & horizonalAxisInput > 0 & horizonalAxisInput > floor_threshold )
                {
                    horizonalAxisInput = pos_floor;
                
                }
            
                if (horizonalAxisInput > -pos_floor & horizonalAxisInput < 0 & horizonalAxisInput < -floor_threshold )
                {
                    horizonalAxisInput = -pos_floor;
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

                playerInput = (forward * verticalAxisInput) + (right * horizonalAxisInput);
                pressJump = InputListener.getJumpAxis() > 0;
                pressSprint = InputListener.getSprintAxis() > 0;
            }
          
        }

        private void FixedUpdate()
        {
            if (isMe)
            {
                move(playerInput, pressJump, pressSprint);
            }
        }


        // Start is called before the first frame update
      

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


                if (doSprint) //if character val and camara are in the exact same direction. i.e parralel i.e cross vector = 0 then sprint
                {
                    playerMovement = (playerInput * Time.deltaTime * (speed + sprintSpeed));
                }
                else
                {
                    playerMovement = (playerInput * Time.deltaTime * speed);
                }
            }

            if (dojump && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY);
            }

            playerVelocity.y += GRAVITY * Time.deltaTime;

            Vector3 targetVelocity = playerMovement + playerVelocity * Time.deltaTime;

            Vector3 move = Vector3.SmoothDamp(characterController.velocity, targetVelocity, ref currentVelocity, smoothTime, MAX_SPEED);
            characterController.Move(targetVelocity);
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