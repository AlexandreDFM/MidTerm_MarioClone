using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class TimerScript : MonoBehaviour
    {
        [FormerlySerializedAs("TimerTxt")] public TextMeshProUGUI timerTxt;
        [FormerlySerializedAs("TimeLeft")] public float timeLeft;
        [FormerlySerializedAs("TimerOn")] public bool timerOn = false;

        private void Start()
        {
            timerOn = true;
        }

        private void Update()
        {
            if (!timerOn) return;
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            } else {
                timeLeft = 0;
                timerOn = false;
            }
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            timerTxt.text = $"{minutes:00}:{seconds:00}";
        }
    }
}