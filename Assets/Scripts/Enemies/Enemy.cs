using Managers;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.CompareTag("PlayerTag")) {
        //         // Play Death Sound
        //         SoundManager.Instance.PlayDeathSound();
        //         // Kill the player
        //
        //         // Destroy(collision.gameObject);
        //         // GameManager.instance.PlayerDies();
        //     }
        // }

        public void TakeDamage(int damage)
        {
            // Play Death Sound
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}