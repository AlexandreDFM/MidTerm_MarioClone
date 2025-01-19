using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.BossScripts
{
    public class Boss : MonoBehaviour
    {
        private static readonly int IsDying = Animator.StringToHash("isDying");
        private static readonly int IsThrowingRock = Animator.StringToHash("isThrowingRock");
        private int _currentHp;
        private Animator _anim;
        private float _throwTimer;
        private float _jumpTimer;

        public GameObject rockPrefab;
        public Transform throwPoint;
        public float throwInterval = 3f;
        public float rockThrowDelay = 0.5f;
        public int maxHp = 10;
        public float jumpIntervalMin = 5f;
        public float jumpIntervalMax = 10f;
        public float jumpForce = 15f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _currentHp = maxHp;
            _rb = GetComponent<Rigidbody2D>();
            _jumpTimer = Random.Range(jumpIntervalMin, jumpIntervalMax);
        }

        private void Update()
        {
            HandleThrowing();
            HandleJumping();
        }

        private void HandleThrowing()
        {
            _throwTimer += Time.deltaTime;
            if (!(_throwTimer >= throwInterval)) return;
            StartThrowingRock();
            _throwTimer = 0;
        }

        private void StartThrowingRock()
        {
            _anim.SetBool(IsThrowingRock, true);
            Invoke(nameof(ThrowRock), rockThrowDelay);
        }

        private void ThrowRock()
        {
            Instantiate(rockPrefab, throwPoint.position, Quaternion.identity);
            _anim.SetBool(IsThrowingRock, false);
        }

        private void HandleJumping()
        {
            _jumpTimer -= Time.deltaTime;
            if (!(_jumpTimer <= 0)) return;
            Jump();
            _jumpTimer = Random.Range(jumpIntervalMin, jumpIntervalMax);
        }

        private void Jump()
        {
            if (_rb.velocity.y == 0) _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D target)
        {
            switch (target.gameObject.tag) {
                case "FireballTag":
                    TakeDamage(1);
                    Destroy(target.gameObject);
                    break;
                case "SawTag":
                    KillBoss();
                    break;
                case "PlayerTag":
                    GameManager.Instance.PlayerTakeDamage();
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            switch (target.gameObject.tag) {
                case "FireballTag":
                    TakeDamage(1);
                    Destroy(target.gameObject);
                    break;
            }
        }

        private void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0) {
                KillBoss();
            }
        }

        private void KillBoss()
        {
            _anim.SetTrigger(IsDying);
            Destroy(gameObject, 1.5f);
        }
    }
}