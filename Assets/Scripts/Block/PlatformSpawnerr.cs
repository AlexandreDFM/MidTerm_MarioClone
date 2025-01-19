using UnityEngine;

namespace Block
{
    public class PlatformSpawner : MonoBehaviour
    {
        public GameObject platformPrefab;
        public float spawnInterval = 3f;
        public Vector2 spawnPosition;
        public float xRange = 5f;
        public int maxPlatforms = 5;
        public float destroyDelay = 10f;

        private float _spawnTimer;
        private int _currentPlatformCount = 0;

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= spawnInterval && _currentPlatformCount < maxPlatforms) {
                SpawnPlatform();
                _spawnTimer = 0f;
            }
        }

        private void SpawnPlatform()
        {
            Vector2 platformSpawnPos = new Vector2(spawnPosition.x + Random.Range(-xRange, xRange), spawnPosition.y);
            GameObject newPlatform = Instantiate(platformPrefab, platformSpawnPos, Quaternion.identity);
            Destroy(newPlatform, destroyDelay);
            _currentPlatformCount++;
            newPlatform.GetComponent<PlatformLifecycle>().OnPlatformDestroyed += HandlePlatformDestroyed;
        }

        private void HandlePlatformDestroyed()
        {
            _currentPlatformCount--;
        }
    }
}