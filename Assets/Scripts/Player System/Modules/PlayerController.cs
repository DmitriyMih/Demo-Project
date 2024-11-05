using UnityEngine;
using Zenject;

namespace GameSystem.PlayerSystem
{
    public class PlayerController : Entity
    {
        [Inject] private PlayerManagment playerManagment;

        [SerializeField] private MovementManagment movementManagment;
        private (bool isAvailable, Vector2 input) movementInput;

        [SerializeField] private LookManagment lookManagment;
        private (bool isAvailable, Vector2 input) lookInput;

        [Header("Body Settings")]
        [SerializeField] private float turnZoneAngle = 45f;
        [SerializeField] private float stabilizationAngle = 2.5f;

        [Space]
        [SerializeField] private float bodyTurnSpeed = 135f;

        private void Awake()
        {
            playerManagment.SetCursorLockState(true);
        }

        public void OnTick()
        {
            lookInput = playerManagment.InputManagment.GetLookInput(lookManagment.HorizontalLookSpeed, 0f);

            if (lookInput.isAvailable)
                lookManagment.HandleLook(lookInput.input);

            movementInput = playerManagment.InputManagment.GetMovementInput();
        }

        public void OnFixedTick()
        {
            AdjustBodyRotation();

            Vector3 movementDirection = CalculateMovementDirection();
            movementManagment.HandleMovement(movementDirection);
        }

        private void AdjustBodyRotation()
        {
            Vector3 headForwardFlattened = Vector3.ProjectOnPlane(lookManagment.Head.forward, Vector3.up).normalized;
            float angleDifference = Vector3.SignedAngle(transform.forward, headForwardFlattened, Vector3.up);

            if (Mathf.Abs(angleDifference) > stabilizationAngle)
            {
                float rotationSpeed = Mathf.Clamp(bodyTurnSpeed * Time.deltaTime, 0f, Mathf.Abs(angleDifference));
                float targetAngle = Mathf.Sign(angleDifference) * rotationSpeed;

                float newAngle = Mathf.LerpAngle(transform.eulerAngles.y, transform.eulerAngles.y + targetAngle, rotationSpeed);

                movementManagment.Body.transform.rotation = Quaternion.Euler(0, newAngle, 0);
                lookManagment.Head.Rotate(Vector3.up, -targetAngle, Space.World);
            }
        }

        private Vector3 CalculateMovementDirection()
        {
            Vector3 inputDirection = playerManagment.InputManagment.ConvertDirectionForInput(movementInput.input);

            Vector3 cameraForward = playerManagment.PlayerCamera.transform.forward;
            Vector3 cameraRight = playerManagment.PlayerCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 movementDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            float angleDifference = Vector3.SignedAngle(movementManagment.Body.transform.forward, lookManagment.Head.forward, Vector3.up);
            if (Mathf.Abs(angleDifference) > turnZoneAngle)
                movementDirection = lookManagment.Head.TransformDirection(inputDirection);

            return movementDirection.normalized;
        }
    }
}