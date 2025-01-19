using Managers;
using UnityEngine;

namespace Flag
{
    public class CheckpointFlag : MonoBehaviour
    {
        public Transform spawnPoint;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerTag")) {
                GameManager.Instance.SetSpawnPoint(spawnPoint);
            }
        }
    }
}