using UnityEngine;

namespace Spatialminds.Platformer
{
    public class MovingObjectManager : MonoBehaviour
    {
        public enum MoveDirection { Left, Right }

        [Header("Movement Settings")]
        [SerializeField] private MoveDirection moveDirection = MoveDirection.Left;
        [SerializeField] private float distance = 3f;
        [SerializeField] private float speed = 2f;

        private Vector3 startPos;
        private Vector3 targetPos;
        private bool movingToTarget = true;
        private Transform carriedPlayer;
        private Vector3 playerOffset;

        void Start()
        {
            startPos = transform.position;
            float dir = moveDirection == MoveDirection.Left ? -1f : 1f;
            targetPos = startPos + new Vector3(distance * dir, 0, 0);
        }

        void Update()
        {
            Move();
            MoveCarriedPlayer();
        }

        void MoveCarriedPlayer()
        {
            if (carriedPlayer != null)
            {
                Vector3 newPos = transform.position + playerOffset;
                newPos.y = carriedPlayer.position.y;
                carriedPlayer.position = newPos;
            }
        }

        void Move()
        {
            Vector3 destination = movingToTarget ? targetPos : startPos;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                movingToTarget = !movingToTarget;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.contacts[0].normal.y < -0.5f)
                {
                    carriedPlayer = collision.transform;
                    playerOffset = carriedPlayer.position - transform.position;
                    playerOffset.y = 0;
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && carriedPlayer != null)
            {
                carriedPlayer = null;
            }
        }

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