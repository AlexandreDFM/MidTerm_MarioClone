using Managers;
using UnityEngine;

namespace Enemies.BossScripts
{
    public class Rock : MonoBehaviour
    {
        public float speed = 5f;
        public float destroyTime = 10f;
        public Transform rockVisual;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
            if (!rockVisual) return;
            rockVisual.Rotate(Vector3.forward * (speed * Time.deltaTime * 100));
        }

        private void OnCollisionEnter2D(Collision2D target)
        {
            if (target.gameObject.CompareTag("PlayerTag"))
                GameManager.Instance.PlayerTakeDamage();
            if (target.gameObject.CompareTag("WallTag")) return;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (target.CompareTag("PlayerTag"))
                GameManager.Instance.PlayerTakeDamage();
            if (target.CompareTag("WallTag") || target.CompareTag("PlatformTag")) return;
            Destroy(gameObject);
        }
    }
}