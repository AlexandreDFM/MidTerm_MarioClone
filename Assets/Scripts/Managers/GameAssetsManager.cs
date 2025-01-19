using UnityEngine;

namespace Managers
{
    public class GameAssetsManager : MonoBehaviour
    {
        public static GameAssetsManager Instance;

        public GameObject fireBall;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }
    }
}