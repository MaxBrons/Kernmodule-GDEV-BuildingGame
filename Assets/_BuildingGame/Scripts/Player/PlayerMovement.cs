using BuildingGame.Core;
using UnityEngine;

namespace BuildingGame.Player
{
    public class PlayerMovement : BaseBehaviour
    {
        private const float walkingSpeed = 7.5f;
        private const float runningSpeed = 11.5f;
        private const float jumpSpeed = 8.0f;
        private const float gravity = 20.0f;
        private Camera playerCamera;
        private const float lookSpeed = 2.0f;
        private const float lookXLimit = 45.0f;

        private GameObject playerPrefab;
        private GameObject player;

        private CharacterController characterController;
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;

        public bool canMove = true;

        public override void OnAwake()
        {
            playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
            player = Object.Instantiate(playerPrefab);
            playerCamera = Camera.main;

            characterController = player.GetComponent<CharacterController>();

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void OnUpdate()
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = player.transform.TransformDirection(Vector3.forward);
            Vector3 right = player.transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = UnityEngine.Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * UnityEngine.Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * UnityEngine.Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (UnityEngine.Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -UnityEngine.Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                player.transform.rotation *= Quaternion.Euler(0, UnityEngine.Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}
