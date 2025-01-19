using UnityEngine;

namespace Block
{
    public class MovingPlatform : MonoBehaviour
    {
        public float speed = 2f;
        public float upperLimit = 10f;
        public bool resetOnLimit = true;

        public Vector2 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            MovePlatform();

            if (!(transform.position.y >= upperLimit)) return;
            if (resetOnLimit) {
                ResetPlatform();
            } else {
                DestroyPlatform();
            }
        }

        private void MovePlatform()
        {
            transform.Translate(Vector2.up * (speed * Time.deltaTime));
        }

        private void ResetPlatform()
        {
            transform.position = startPosition;
        }

        private void DestroyPlatform()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) {
                collision.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) {
                collision.transform.SetParent(null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(transform.position.x - 1f, upperLimit),
                new Vector2(transform.position.x + 1f, upperLimit));
        }
    }
}