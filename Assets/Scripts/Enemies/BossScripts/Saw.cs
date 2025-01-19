using UnityEngine;

namespace Enemies.BossScripts
{
    public class Saw : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private bool _isTriggered = false;

        private void Update()
        {
            if (_isTriggered) {
                MoveLeft();

                // In development

                // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
                // bool isPlatform = hit.collider != null && hit.collider.gameObject.CompareTag("PlatformTag");
                // if (!isPlatform) {
                //     Destroy(hit.collider.gameObject);
                // }
            }
        }

        private void MoveLeft()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.tag) {
                case "PlayerTag":
                    _isTriggered = true;
                    break;
                case "PlatformTag":
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}