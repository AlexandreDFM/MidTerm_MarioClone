using UnityEngine;

namespace PowerUps
{
    public class Mushroom : MonoBehaviour
    {
        private Rigidbody2D _myBody;
        private float _speed = 2f;
        private float _direction = 1f;

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _myBody.velocity = new Vector2(0f, -2f);
        }

        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("PlayerTag")) return;
            Destroy(gameObject);
            collision.gameObject.GetComponent<Player.Player>().Grow();
        }

        private void Move()
        {
            _myBody.velocity = new Vector2(_speed * _direction, _myBody.velocity.y);
        }

        private void ChangeDirection()
        {
            _direction *= -1;
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (!target.CompareTag("PlayerTag")) ChangeDirection();
        }

        private void CheckBounds()
        {
            if (transform.position.y < -6f) Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Move();
            CheckBounds();
        }
    }
}