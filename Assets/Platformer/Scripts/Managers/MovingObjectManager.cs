using UnityEngine;

namespace Spatialminds.Platformer
{
    public class MovingObjectManager : MonoBehaviour
    {
        public enum MoveDirection { Left, Right }

        [Header("Movement Settings")]
        [SerializeField] private MoveDirection moveDirection = MoveDirection.Left;
        [SerializeField] private float distance = 3f; // How many units to move
        [SerializeField] private float speed = 2f;

        private Vector3 startPos;
        private Vector3 targetPos;
        private bool movingToTarget = true;

        void Start()
        {
            startPos = transform.position;
            float dir = moveDirection == MoveDirection.Left ? -1f : 1f;
            targetPos = startPos + new Vector3(distance * dir, 0, 0);
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            Vector3 destination = movingToTarget ? targetPos : startPos;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                movingToTarget = !movingToTarget; // Reverse direction
            }
        }

        // Optional: Draw gizmos to visualize movement in the editor
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            float dir = moveDirection == MoveDirection.Left ? -1f : 1f;
            Vector3 previewTarget = transform.position + new Vector3(distance * dir, 0, 0);
            Gizmos.DrawLine(transform.position, previewTarget);
            Gizmos.DrawWireCube(previewTarget, transform.localScale);
        }
    }
}
