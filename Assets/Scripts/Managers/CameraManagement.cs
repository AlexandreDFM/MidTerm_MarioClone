using UnityEngine;

namespace Managers
{
    public class CameraManagement : MonoBehaviour
    {
        public static CameraManagement Instance;
        public Transform target;

        public float distance = -10f;
        public float height = 0f;
        public float damping = 5.0f;
        public float mapX = 200.0f;
        public float mapY = 100.0f;

        private float _minX = 0f;
        private float _maxX = 0f;
        private float _minY = 0f;
        private float _maxY = 0f;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                TransferDataToExistingInstance();
                Destroy(gameObject);
            }

            _minX = transform.position.x;
            _minY = transform.position.y;

            _maxX = mapX;
            _maxY = mapY;
        }

        private void TransferDataToExistingInstance()
        {
            if (target) Instance.target = target;
            if (!Mathf.Approximately(distance, -10f)) Instance.distance = distance;
            if (height != 0f) Instance.height = height;
            if (!Mathf.Approximately(damping, 5.0f)) Instance.damping = damping;
            if (!Mathf.Approximately(mapX, 200.0f)) Instance.mapX = mapX;
            if (!Mathf.Approximately(mapY, 100.0f)) Instance.mapY = mapY;
        }

        private void Update()
        {
            if (!target) return;
            Vector3 wantedPosition = target.TransformPoint(0, height, distance);

            wantedPosition.x = (wantedPosition.x < _minX) ? _minX : wantedPosition.x;
            wantedPosition.x = (wantedPosition.x > _maxX) ? _maxX : wantedPosition.x;

            wantedPosition.y = (wantedPosition.y < _minY) ? _minY : wantedPosition.y;
            wantedPosition.y = (wantedPosition.y > _maxY) ? _maxY : wantedPosition.y;

            transform.position = Vector3.Lerp(transform.position, wantedPosition, (Time.deltaTime * damping));
        }
    }
}