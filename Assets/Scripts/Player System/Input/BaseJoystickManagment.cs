using UnityEngine;

namespace GameSystem.JoystickSystem
{
    public abstract class BaseJoystickManagment : ICustomTickable
    {
        [field: SerializeField] public int InputID { get; private set; }

        [Space]
        [SerializeField] protected BaseJoystick joystick;

        [Header("Temp Settings")]
        [SerializeField] protected float tapTime = 0.2f;
        [SerializeField] protected float currentPressedTime;
        [SerializeField] protected bool isPressed;

        public float CurrentPressedTime => currentPressedTime;
        public bool IsPressed => isPressed;

        public abstract (bool isAvailable, bool isTap, Vector2 direction) GetInput(Transform content, bool isInputMobile);

        public void OnTick()
        {
            if (IsPressed)
                currentPressedTime += Time.deltaTime;
        }

        public void OnResetTime() => currentPressedTime = 0f;
        public bool CheckTap() => currentPressedTime < tapTime;

        protected (bool isAvailable, bool isTap, Vector2 direction, float lenghtValue) GetJoystickInput(BaseJoystick joystick, ref bool isPressed, float? deadZone)
        {
            bool isTap = joystick.PressedTime <= tapTime;
            isPressed = joystick.IsPressed;

            return (isPressed, isTap, joystick.Direction.normalized, joystick.Direction.magnitude);
        }

        protected (bool state, Vector2 input) GetInputByPriority(bool isFirstElementPriority, (bool state, Vector2 input) element1, (bool state, Vector2 input) element2)
        {
            return isFirstElementPriority ?
                (element1.state ? element1 : element2.state ? element2 : (false, Vector2.zero)) :
                (element2.state ? element2 : element1.state ? element1 : (false, Vector2.zero));
        }

        protected Vector2 GetInputVector(Vector3 touchPosition, Transform content)
        {
            touchPosition.y = content.position.y;
            Vector3 direction = touchPosition - content.position;
            Vector2 directionXZ = new Vector2(direction.x, direction.z).normalized;

            //Clamp the direction to be between -1 and 1
            directionXZ.x = Mathf.Clamp(directionXZ.x, -1, 1);
            directionXZ.y = Mathf.Clamp(directionXZ.y, -1, 1);

            return directionXZ;
        }
    }
}