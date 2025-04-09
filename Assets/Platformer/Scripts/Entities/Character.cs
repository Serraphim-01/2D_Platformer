using UnityEngine;

namespace Spatialminds.Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;

        [Header("Jump")]
        [SerializeField] private float jumpVelocity;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Vector2 groundCheckOrigin;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask deathLayer;

        [Header("Falling")]
        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float lowJumpMultiplier = 2f;
        [SerializeField] private float highJumpMultiplier = 2f;
        [SerializeField] private bool clampFallSpeed = false;
        [SerializeField] private float fallSpeedClamp = 50f;

        [Header("Finish Menu")]
        [SerializeField] private GameObject finishMenuUI;


        public float horizontal { get; private set; }
        bool isJumpPressed;
        public Rigidbody2D characterRB { get; private set; }
        Vector2 direction;
        bool facingRight = true;
        public CharacterStateManager characterStateManager { get; private set; }
        [SerializeField] private CharacterAnimatorManager characterAnim;
        public CharacterAnimatorManager CharacterAnim() => characterAnim;


        public bool isGround { get; private set; }
        private int remainingAirJumps = 1;

        void Awake()
        {
            characterRB = GetComponent<Rigidbody2D>();
            characterStateManager = new CharacterStateManager(this);
        }

        public virtual void Start()
        {
            characterStateManager.Initialize(characterStateManager.idleState);
            finishMenuUI.SetActive(false);
        }

        public virtual void Update()
        {
            CheckIsGround();
            UpdateCharacterDirection();
            UpdateFall();

            characterStateManager.Update();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & deathLayer) != 0)
            {
                Die();
                finishMenuUI.SetActive(true);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                ScoreManager.Instance.AddScore(1);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Finish"))
            {
                if (finishMenuUI != null)
                    finishMenuUI.SetActive(true);

                Time.timeScale = 0f;
            }
        }

        public void Die()
        {
            Destroy(gameObject);
            finishMenuUI.SetActive(true);
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        internal void SetHorizontal(float x) => horizontal = x;

        internal void SetJumpPressed(bool value) => isJumpPressed = value;

        public void MovePlayer()
        {
            direction = transform.right * horizontal;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, moveSpeed * Time.deltaTime);
        }

        // Added a Double Jump Feature
        internal void Jump()
        {
            if (isGround)
            {
                characterRB.velocity += Vector2.up * jumpVelocity;
                remainingAirJumps = 1; // Reset when jumping from ground
            }
            else if (remainingAirJumps > 0)
            {
                characterRB.velocity += Vector2.up * jumpVelocity;
                remainingAirJumps = 0; // Use up air jump
            }
        }

        void CheckIsGround()
        {
            isGround = Physics2D.Raycast((Vector2)transform.position + groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);
        }

        void UpdateFall()
        {
            // This means the player is falling
            if (characterRB.velocity.y < 0)
            {
                characterRB.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
                //characterRB.velocity = new Vector3(characterRB.velocity.x, Mathf.Clamp(characterRB.velocity.y, ))
            }
            else if (characterRB.velocity.y > 0 && !isJumpPressed)
            {
                characterRB.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
            }
            else if (characterRB.velocity.y > 0 && isJumpPressed)
            {
                characterRB.velocity += (highJumpMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
            }
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        void UpdateCharacterDirection()
        {
            if (facingRight == false && horizontal > 0)
            {
                Flip();
            }
            else if (facingRight == true && horizontal < 0)
            {
                Flip();
            }
        }
    }
}
