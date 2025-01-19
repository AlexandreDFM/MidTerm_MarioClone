using Managers;
using UnityEngine;

namespace Enemies
{
    public class BeetleDirection : MonoBehaviour
    {
        private static readonly int IsStunned = Animator.StringToHash("IsStunned");
        private Rigidbody2D _myBody;
        private Animator _anim;
        private bool _isStunned;
        private bool _isFacingRight = false;
        private const float Speed = 1f;

        [SerializeField] private Transform edgeCheck;
        [SerializeField] private LayerMask groundLayer;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _isStunned = false;
        }

        private void Update()
        {
            BeetleWalk();
        }

        private void BeetleWalk()
        {
            if (_isStunned) {
                _myBody.velocity = new Vector2(0f, _myBody.velocity.y);
                return;
            }

            if (edgeCheck) {
                bool isAtEdge = !Physics2D.Raycast(edgeCheck.position, Vector2.down, 0.1f, groundLayer);
                if (isAtEdge) ChangeDirection();
            }

            _myBody.velocity = new Vector2(Speed * (_isFacingRight ? 1 : -1), _myBody.velocity.y);
        }

        private void ChangeDirection()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x = -tempScale.x;
            transform.localScale = tempScale;
        }

        private void OnCollisionEnter2D(Collision2D target)
        {
            if (target.gameObject.CompareTag("PlayerTag") && target.contacts[0].normal.y < -0.5f) DestroyBeetle();
            else if (target.gameObject.CompareTag("PlayerTag")) KillPlayer();
            else if (!target.gameObject.CompareTag("WallTag")) ChangeDirection();
        }

        private void DestroyBeetle()
        {
            _anim.SetBool(IsStunned, true);
            SoundManager.Instance.PlaySquishSound();
            Destroy(gameObject, 0.25f);
        }

        private static void KillPlayer()
        {
            GameManager.Instance.PlayerTakeDamage();
        }
    }
}