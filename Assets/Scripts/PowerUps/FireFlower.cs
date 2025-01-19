using UnityEngine;

namespace PowerUps
{
    public class FireFlower : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision");
            if (!collision.gameObject.CompareTag("PlayerTag")) return;
            Destroy(gameObject);
            collision.gameObject.GetComponent<Player.Player>().Fire();
        }
    }
}