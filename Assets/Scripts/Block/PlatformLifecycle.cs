using System;
using UnityEngine;

namespace Block
{
    public class PlatformLifecycle : MonoBehaviour
    {
        public event Action OnPlatformDestroyed;

        private void OnDestroy()
        {
            OnPlatformDestroyed?.Invoke();
        }
    }
}