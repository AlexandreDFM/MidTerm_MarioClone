using UnityEngine;

namespace Collectables
{
    public class Collectables : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }
    }
}