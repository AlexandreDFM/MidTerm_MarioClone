using Managers;
using UnityEngine;

namespace Enemies
{
    public class HedgehogDirection : MonoBehaviour
    {
        private Animator _anim;
        private Rigidbody2D _myBody;
        private bool _isFacingRight = false;
        private readonly float _speed = 1f;

        [SerializeField] private Transform edgeCheck;
        [SerializeField] private LayerMask groundLayer;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            HedgehogWalk();
        }

        private void HedgehogWalk()
        {
            if (edgeCheck) {
                bool isAtEdge = !Physics2D.Raycast(edgeCheck.position, Vector2.down, 0.1f, groundLayer);

                if (isAtEdge) {
                    ChangeDirection();
                }
            }

            _myBody.velocity = new Vector2(_speed * (_isFacingRight ? 1 : -1), _myBody.velocity.y);
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
            if (target.gameObject.CompareTag("PlayerTag")) {
                KillPlayer();
            } else if (!target.gameObject.CompareTag("WallTag")) {
                ChangeDirection();
            }
        }

        private static void KillPlayer()
        {
            GameManager.Instance.PlayerTakeDamage();
        }
    }
}