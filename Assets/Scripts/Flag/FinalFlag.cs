using Managers;
using UnityEngine;

namespace Flag
{
    public class BossFlag : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerTag")) {
                GameManager.LevelComplete();
            }
        }
    }
}