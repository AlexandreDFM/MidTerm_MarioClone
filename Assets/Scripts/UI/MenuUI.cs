using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        public int levelToLoad = 1;
        public Button levelQuitButton;
        public Button levelMenuButton;
        public Button levelSelectButton;
        public Button levelRestartButton;

        public Slider volumeSlider;
        public Slider musicVolumeSlider;

        private void OnEnable()
        {
            if (Managers.SoundManager.Instance == null) {
                Debug.LogError("SoundManager instance is null. Make sure SoundManager is loaded in the scene.");
                return;
            }

            if (levelSelectButton) {
                levelSelectButton.onClick.RemoveAllListeners();
                levelSelectButton.onClick.AddListener(() => Managers.MySceneManager.Instance.PlayLevel(levelToLoad));
            }

            if (levelQuitButton) {
                levelQuitButton.onClick.RemoveAllListeners();
                levelQuitButton.onClick.AddListener(() => Managers.MySceneManager.Instance.QuitGame());
            }

            if (levelMenuButton) {
                levelMenuButton.onClick.RemoveAllListeners();
                levelMenuButton.onClick.AddListener(() => Managers.MySceneManager.Instance.LoadMainMenu());
            }

            if (levelRestartButton) {
                levelRestartButton.onClick.RemoveAllListeners();
                levelRestartButton.onClick.AddListener(() => Managers.MySceneManager.Instance.RestartGame());
            }

            if (volumeSlider) {
                volumeSlider.onValueChanged.RemoveAllListeners();
                volumeSlider.onValueChanged.AddListener((value) => Managers.SoundManager.Instance.ChangeVolume(value));

                volumeSlider.value = Managers.SoundManager.Instance.soundVolume;
            }

            if (musicVolumeSlider) {
                musicVolumeSlider.onValueChanged.RemoveAllListeners();
                musicVolumeSlider.onValueChanged.AddListener((value) =>
                    Managers.SoundManager.Instance.ChangeMusicVolume(value));

                musicVolumeSlider.value = Managers.SoundManager.Instance.musicVolume;
            }
        }
    }
}