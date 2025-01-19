using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using UnityEngine;

    public class BackgroundUI : MonoBehaviour
    {
        public static BackgroundUI Instance;

        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private Sprite defaultSprite;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Instance.SetBackground(defaultSprite);
                Destroy(gameObject);
                return;
            }

            if (!backgroundRenderer) {
                Debug.LogError("Background SpriteRenderer component is not assigned!");
            }

            if (defaultSprite) {
                SetBackground(defaultSprite);
            }
        }

        public void SetBackground(Sprite newSprite)
        {
            if (backgroundRenderer != null && newSprite != null) {
                backgroundRenderer.sprite = newSprite;
            } else {
                Debug.LogWarning("Background SpriteRenderer or Sprite is missing. Cannot update background.");
            }
        }
    }
}