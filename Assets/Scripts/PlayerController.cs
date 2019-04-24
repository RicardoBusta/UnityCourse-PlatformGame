using NaughtyAttributes;

namespace Game
{
    using UnityEngine;

// [ ] Menu inicial para o jogo

// Criar um ou mais streaming assets para o projeto
// Usar streaming assets pos build tornando o projeto customizável

// Criar um level 2 para o projeto
// Criar um asset bundle com os assets presentes no level 2
// Carregar o asset bundle entre o level 1 e o level 2
// Usar assets carregadors no level 2

    public class PlayerController : MonoBehaviour
    {
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

        private static readonly float upThreshold = 0.5f;

        public VirtualGamePad GamePad;

        public int Life;
        private int CurrentLife;

        public GameController Controller;

        [BoxGroup("ScriptReferences")] [Required]
        public Rigidbody2D RigidBody;

        [BoxGroup("ScriptReferences")] [Required]
        public SpriteRenderer SpriteRenderer;

        [BoxGroup("ScriptReferences")] [Required]
        public Animator Animator;

        [BoxGroup("ScriptReferences")] [Required]
        public AudioSource AudioSource;

        [BoxGroup("Assets")] [Required] public AudioClip JumpSound;

        [BoxGroup("Assets")] [Required] public AudioClip LandSound;

        [BoxGroup("Parameters")] public float acceleration = 5;

        [BoxGroup("Parameters")] public float maxVelocity = 5;

        [BoxGroup("Parameters")] public float jumpSpeed = 5;

        private Vector2 playerInput;
        private bool grounded;
        private bool canJump;
        private float timeSinceJump = 0;

        private void Start()
        {
            CurrentLife = Life;
            Controller.SetMaxLife(Life);
        }

        private void Update()
        {
            playerInput = GamePad.Axis;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            MovementLogic();
        }

        private void MovementLogic()
        {
            var hForce = playerInput.x * acceleration;

            if (!grounded)
            {
                timeSinceJump += Time.fixedDeltaTime;
            }

            if (Mathf.Approximately(hForce, 0))
            {
                hForce = -RigidBody.velocity.x;
            }

            RigidBody.AddForce(new Vector2(hForce, 0), ForceMode2D.Impulse);

            if (!Mathf.Approximately(playerInput.x, 0))
            {
                SpriteRenderer.flipX = playerInput.x < 0;
            }

            var vel = RigidBody.velocity;

            if (canJump && playerInput.y > 0)
            {
                vel.y = jumpSpeed;
                grounded = false;
                canJump = false;
                PlayJumpSound();
            }

            vel.x = Mathf.Clamp(vel.x, -maxVelocity, maxVelocity);
            RigidBody.velocity = vel;

            Animator.SetBool(Moving, Mathf.Abs(vel.x) > 0.1f);
            Animator.SetBool(Jumping, !grounded);
            Animator.SetFloat(VerticalVelocity, vel.y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                var item = other.GetComponent<CollectibleItem>();
                if (item != null)
                {
                    item.GetItem();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    var topHit = enemy.Hit(other.contacts);

                    if (topHit)
                    {
                        RigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                    }
                    else
                    {
                        Die();
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (timeSinceJump > 0.2f && RigidBody.velocity.y <= 0 && !grounded &&
                other.gameObject.CompareTag("Ground"))
            {
                foreach (var contact in other.contacts)
                {
                    if (Vector2.Dot(contact.normal, Vector2.up) > upThreshold)
                    {
                        grounded = true;
                        canJump = true;
                        timeSinceJump = 0;
                        break;
                    }
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = false;
            }
        }

        private void Die()
        {
            CurrentLife--;
            if (CurrentLife == 0)
            {
                Controller.Lose();
                Time.timeScale = 0;
            }

            Controller.SetLife(CurrentLife);
            RigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }

        private void PlayJumpSound()
        {
            AudioSource.clip = JumpSound;
            AudioSource.volume = 0.3f;
            AudioSource.Play();
        }
    }
}