using Managers;
using UnityEngine;

namespace Block
{
    public class DestroyBlock : MonoBehaviour
    {
        public GameObject blockParticle;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("PlayerTag") || !(collision.contacts[0].normal.y > 0.5f)) return;
            GameManager.Instance.UpdateScore(10);
            SoundManager.Instance.PlayBlockSound();
            // Instantiate(blockParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}