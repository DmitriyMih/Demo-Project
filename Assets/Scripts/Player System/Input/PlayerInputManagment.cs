using UnityEngine;
using GameSystem.JoystickSystem;

namespace GameSystem.PlayerSystem
{
    [System.Serializable]
    public class PlayerInputManagment
    {
        [SerializeField] private MovementJoystickManagment movement;
        private Vector2 lastLookTouchPosition;

        [field: Space]
        [field: SerializeField] public bool IsInputMobile { get; private set; }

        public (bool isAvailable, Vector2 input) GetLookInput(float horizontalLookSpeed, float verticalLookSpeed)
        {
            if (!IsInputMobile)
            {
                Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X") * horizontalLookSpeed, Input.GetAxis("Mouse Y") * verticalLookSpeed);
                return (mouseInput != Vector2.zero, mouseInput);
            }
            else
            {
                if (Input.touchCount == 0) return default;

                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    lastLookTouchPosition = touch.position;
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 deltaPosition = touch.position - lastLookTouchPosition;
                    lastLookTouchPosition = touch.position;

                    Vector2 input = new Vector2()
                    {
                        x = deltaPosition.x * horizontalLookSpeed * 0.01f,
                        y = deltaPosition.y * verticalLookSpeed * 0.01f
                    };

                    return (input != Vector2.zero, input);
                }

                return default;
            }
        }

        public (bool isAvailable, Vector2 direction) GetMovementInput() => movement.GetMovementInput(IsInputMobile);
        public Vector3 ConvertDirectionForInput(Vector2 input) => new Vector3(input.x, 0f, input.y);
    }
}