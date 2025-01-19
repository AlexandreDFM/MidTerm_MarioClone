using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _initialDirection = Vector2.right;

    public int damage = 1;
    public float speed = 8f;
    public int maxBounces = 3;
    public int bounceCount = 0;
    public LayerMask groundLayer;
    public float bounceForce = 6f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyTag")) {
            collision.GetComponent<Enemies.Enemy>().TakeDamage(damage);
            DestroyFireball();
        } else if (collision.CompareTag("WallTag") || collision.CompareTag("PlatformTag")) {
            Bounce();
        }
    }

    private void Bounce()
    {
        bounceCount++;
        if (bounceCount > maxBounces) {
            DestroyFireball();
        } else {
            _rb.velocity = new Vector2(_rb.velocity.x, bounceForce);
        }
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        _initialDirection = direction;
        _rb.velocity = new Vector2(speed * _initialDirection.x, _rb.velocity.y);
    }
}