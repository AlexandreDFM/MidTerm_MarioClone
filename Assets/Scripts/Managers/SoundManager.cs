using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioSource audioBackgroundSource;
        public AudioSource audioEffectSource;

        public float soundVolume = 5f;
        public float musicVolume = 5f;

        public float minVolume = 0f;
        public float maxVolume = 10f;

        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip coinSound;
        [SerializeField] private AudioClip kickSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip blockSound;
        [SerializeField] private AudioClip squishSound;
        [SerializeField] private AudioClip backgroundMusic;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                TransferDataToExistingInstance();
                Destroy(gameObject);
                return;
            }

            audioBackgroundSource = gameObject.GetComponent<AudioSource>();
            audioEffectSource = gameObject.AddComponent<AudioSource>();
            PlayBackgroundMusic();
        }

        private void TransferDataToExistingInstance()
        {
            if (jumpSound) Instance.jumpSound = jumpSound;
            if (coinSound) Instance.coinSound = coinSound;
            if (kickSound) Instance.kickSound = kickSound;
            if (deathSound) Instance.deathSound = deathSound;
            if (blockSound) Instance.blockSound = blockSound;
            if (squishSound) Instance.squishSound = squishSound;
            if (backgroundMusic) Instance.backgroundMusic = backgroundMusic;

            Instance.PlayBackgroundMusic();
        }

        public void PlayJumpSound()
        {
            audioEffectSource.PlayOneShot(jumpSound);
        }

        public void PlayCoinSound()
        {
            audioEffectSource.PlayOneShot(coinSound);
        }

        public void PlayDeathSound()
        {
            audioEffectSource.PlayOneShot(deathSound);
        }

        public void PlaySquishSound()
        {
            if (!audioEffectSource) return;
            audioEffectSource.volume = 2f;
            audioEffectSource.PlayOneShot(squishSound);
            audioEffectSource.volume = 1f;
        }

        public void PlayKickSound()
        {
            audioEffectSource.PlayOneShot(kickSound);
        }

        public void PlayBlockSound()
        {
            audioEffectSource.PlayOneShot(blockSound);
        }

        private void PlayBackgroundMusic()
        {
            if (audioBackgroundSource.clip == backgroundMusic) return;
            audioBackgroundSource.clip = backgroundMusic;
            audioBackgroundSource.loop = true;
            audioBackgroundSource.Play();
        }

        public void StopBackgroundMusic()
        {
            audioBackgroundSource.Stop();
        }

        public void ChangeVolume(float value)
        {
            soundVolume = value;
            audioEffectSource.volume = soundVolume / maxVolume;
        }

        public void ChangeMusicVolume(float value)
        {
            musicVolume = value;
            audioBackgroundSource.volume = musicVolume / maxVolume;
        }
    }
}