using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MySceneManager : MonoBehaviour
    {
        public static MySceneManager Instance;

        private void Start()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync("MarioLevelOne");
        }

        public void PlayLevel(int level)
        {
            SceneManager.LoadSceneAsync("level_" + level);
        }

        public void PlayLevelScene(string level)
        {
            SceneManager.LoadSceneAsync(level);
        }

        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync("menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}