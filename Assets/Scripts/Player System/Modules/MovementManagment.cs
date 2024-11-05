using UnityEngine;

namespace GameSystem.PlayerSystem
{
    [System.Serializable]
    public class MovementManagment
    {
        [field: SerializeField] public Rigidbody Body { get; private set; }
        [SerializeField] private float movementSpeed;

        public void HandleMovement(Vector3 movementDirection)
        {
            float moveDistance = movementSpeed * Time.deltaTime;
            Vector3 movement = movementDirection.normalized * moveDistance;
            Body.MovePosition(Body.transform.position + movement);
        }
    }
}