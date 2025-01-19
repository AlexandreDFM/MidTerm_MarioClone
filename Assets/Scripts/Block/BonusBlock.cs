using Managers;
using UnityEngine;

namespace Block
{
    public class BonusBlock : MonoBehaviour
    {
        public GameObject powerUp;
        private static readonly int IsHit = Animator.StringToHash("IsHit");

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("PlayerTag") || GetComponent<Animator>().GetBool(IsHit) ||
                collision.contacts[0].normal.y <= 0.5f) return;
            SoundManager.Instance.PlayCoinSound();
            GameManager.Instance.UpdateCoinCount(1);
            GetComponent<Animator>().SetBool(IsHit, true);
            if (powerUp == null) return;
            GameObject instantiatedPowerUp = Instantiate(powerUp,
                transform.position + new Vector3(0f, powerUp.GetComponent<Renderer>().bounds.size.y, 0f),
                Quaternion.identity);
            instantiatedPowerUp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}