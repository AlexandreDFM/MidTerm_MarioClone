using Managers;
using UnityEngine;

namespace Flag
{
    public class FinalFlag : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerTag")) {
                GameManager.LevelComplete();
            }
        }
    }
}