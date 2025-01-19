using UnityEngine;
using System.Collections;

namespace Block
{
    public class LavaCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlayerTag")) {
                Managers.GameManager.Instance.PlayerTakeDamage();
            }
        }
    }
}