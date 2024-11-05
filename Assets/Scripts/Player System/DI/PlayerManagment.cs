using UnityEngine;

namespace GameSystem.PlayerSystem
{
    [System.Serializable]
    public class PlayerManagment : ICustomTickable
    {
        [field: SerializeField] public Camera PlayerCamera { get; private set; }
        [field: SerializeField] public PlayerController PlayerController { get; private set; }
        [field: SerializeField] public PlayerInputManagment InputManagment { get; private set; }

        public void SetCursorLockState(bool isLock) => Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;

        public void OnTick()
        {
            if (PlayerController != null)
                PlayerController.OnTick();
        }

        public void OnFixedTick()
        {
            if (PlayerController != null)
                PlayerController.OnFixedTick();
        }
    }
}