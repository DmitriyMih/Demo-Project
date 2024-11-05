using UnityEngine;

namespace GameSystem.JoystickSystem
{
    [System.Serializable]
    public class MovementJoystickManagment : BaseJoystickManagment
    {
        [SerializeField] private float lerpSpeed;

        public override (bool isAvailable, bool isTap, Vector2 direction) GetInput(Transform content, bool isInputMobile)
        {
            var movement = GetMovementInput(isInputMobile);
            return (movement.isAvailable, CheckTap(), movement.direction);
        }

        public (bool isAvailable, Vector2 direction) GetMovementInput(bool isInputMobile)
        {
            //Vector2 pcDirection = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 pcDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                pcDirection.x = -1;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                pcDirection.x = 1;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                pcDirection.y = 1;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                pcDirection.y = -1;

            (bool state, Vector2 input) pcResult = (pcDirection != Vector2.zero, pcDirection);

            if (joystick == null)
                return pcResult;

            var joyInput = GetJoystickInput(joystick, ref isPressed, null);
            (bool state, Vector2 input) mobileResult = (joyInput.isAvailable, joyInput.direction);

            var input = GetInputByPriority(isInputMobile, mobileResult, pcResult);
            return (input.state, input.input);
        }
    }
}