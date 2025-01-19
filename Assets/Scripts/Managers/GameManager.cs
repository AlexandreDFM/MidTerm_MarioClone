using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _score = 0;
        private int _coinCount = 0;

        public static GameManager Instance;

        public int lifeCount = 3;
        public Transform textCoin;
        public Transform textLife;
        public Transform textScore;
        public Transform spawnPoint;

        public Transform playerPrefab;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            } else {
                TransferDataToExistingInstance();
                Destroy(gameObject);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            if (textCoin) {
                textCoin.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + _coinCount.ToString();
            }

            if (textLife) {
                textLife.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + lifeCount.ToString();
            }

            if (textScore) {
                textScore.GetComponent<TMPro.TextMeshProUGUI>().text = _score.ToString();
            }

            if (playerPrefab && spawnPoint) {
                playerPrefab.position = spawnPoint.position;
            }
        }

        private void TransferDataToExistingInstance()
        {
            if (textCoin) Instance.textCoin = textCoin;
            if (textLife) Instance.textLife = textLife;
            if (textScore) Instance.textScore = textScore;
            if (spawnPoint) Instance.spawnPoint = spawnPoint;
            if (playerPrefab) Instance.playerPrefab = playerPrefab;

            Instance.UpdateUI();
        }

        private void UpdateUI()
        {
            if (textCoin) textCoin.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + _coinCount.ToString();
            if (textLife) textLife.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + lifeCount.ToString();
            if (textScore) textScore.GetComponent<TMPro.TextMeshProUGUI>().text = _score.ToString();
        }

        public void UpdateCoinCount(int value)
        {
            if (!textCoin) return;
            _coinCount += value;
            textCoin.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + _coinCount.ToString();
        }

        public void UpdateScore(int value)
        {
            if (!textScore) return;
            _score += value;
            textScore.GetComponent<TMPro.TextMeshProUGUI>().text = _score.ToString();
        }

        public void PlayerTakeDamage()
        {
            if (playerPrefab.GetComponent<Player.Player>().GetPlayerHp() > 1) {
                playerPrefab.GetComponent<Player.Player>().Shrink();
            } else {
                PlayerDies();
            }
        }

        private void PlayerDies()
        {
            var actualSpawnPoint = spawnPoint;
            playerPrefab.GetComponent<Player.Player>().DeadAnimation();
            lifeCount--;

            if (lifeCount > 0) {
                playerPrefab.position = spawnPoint.position;
            } else {
                GameOver();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (lifeCount <= 0) return;
            spawnPoint = actualSpawnPoint;
            playerPrefab.position = spawnPoint.position;
        }

        private void GameOver()
        {
            SceneManager.LoadScene("menu");
            lifeCount = 3;
        }

        public static void LevelComplete()
        {
            SceneManager.LoadScene("menu");
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void SetSpawnPoint(Transform newSpawnPoint)
        {
            spawnPoint = newSpawnPoint;
        }
    }
}