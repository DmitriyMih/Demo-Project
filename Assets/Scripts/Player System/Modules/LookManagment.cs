using UnityEngine;

namespace GameSystem.PlayerSystem
{
    [System.Serializable]
    public class LookManagment
    {
        [field: Header("Look Settings")]
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public float HorizontalLookSpeed { get; private set; } = 2f;
        [HideInInspector] public float VerticalLookSpeed { get; private set; } = 2f;

        public void HandleLook(Vector2 inputValue)
        {
            if (Head != null)
                Head.rotation *= Quaternion.Euler(inputValue.y, inputValue.x, 0);
        }
    }
}