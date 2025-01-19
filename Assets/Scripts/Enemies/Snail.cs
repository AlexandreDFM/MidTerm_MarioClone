using Managers;
using UnityEngine;

namespace Enemies
{
    public class SnailDirection : MonoBehaviour
    {
        private Animator _anim;
        private Rigidbody2D _myBody;
        private bool _isKicked;
        private bool _isInShell;
        private float _currentSpeed;
        private const float KickSpeed = 8f;
        private const float WalkSpeed = -1f;
        private static readonly int IsStunned = Animator.StringToHash("IsStunned");

        public Transform platformCheck;
        public LayerMask groundLayer;
        public float platformCheckRadius = 0.2f;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _isInShell = false;
            _isKicked = false;
            _currentSpeed = WalkSpeed;
        }

        private void FixedUpdate()
        {
            if (_isInShell && _isKicked) {
                _myBody.velocity = new Vector2(_currentSpeed, _myBody.velocity.y);
            } else if (!_isInShell) {
                SnailWalk();
                CheckForPlatformEdge();
            }
        }

        private void SnailWalk()
        {
            _myBody.velocity = _anim.GetBool(IsStunned)
                ? new Vector2(0f, _myBody.velocity.y)
                : new Vector2(_currentSpeed, _myBody.velocity.y);
        }

        private void CheckForPlatformEdge()
        {
            if (!platformCheck) return;

            bool isAtEdge = !Physics2D.OverlapCircle(platformCheck.position, platformCheckRadius, groundLayer);

            if (isAtEdge) {
                ChangeDirection(-1 * (int)Mathf.Sign(_currentSpeed));
                _currentSpeed = -_currentSpeed;
            }
        }

        private void ChangeDirection(int direction)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction * Mathf.Abs(tempScale.x);
            transform.localScale = tempScale;
        }

        private void OnCollisionEnter2D(Collision2D target)
        {
            if (target.gameObject.CompareTag("PlayerTag")) {
                if (target.contacts[0].normal.y < -0.5f) {
                    if (!_isInShell) {
                        EnterShell();
                    } else if (!_isKicked) {
                        KickShell(target);
                    }
                } else {
                    if (_isInShell) {
                        KickShell(target);
                    } else if (!_isKicked) {
                        KillPlayer();
                    }
                }
            } else if (!target.gameObject.CompareTag("WallTag")) {
                ChangeDirection((int)Mathf.Sign(_currentSpeed));
                _currentSpeed = -_currentSpeed;
            }
        }

        private void EnterShell()
        {
            _isInShell = true;
            _anim.SetBool(IsStunned, true);
            SoundManager.Instance.PlaySquishSound();
            _currentSpeed = 0f;
            _myBody.velocity = Vector2.zero;
        }

        private void KickShell(Collision2D target)
        {
            _isKicked = true;
            SoundManager.Instance.PlayKickSound();
            _currentSpeed = KickSpeed * Mathf.Sign(target.transform.position.x - transform.position.x);
        }

        private static void KillPlayer()
        {
            GameManager.Instance.PlayerTakeDamage();
        }

        private void OnDestroy()
        {
            SoundManager.Instance.PlaySquishSound();
        }

        private void OnDrawGizmos()
        {
            if (!platformCheck) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(platformCheck.position, platformCheckRadius);
        }
    }
}