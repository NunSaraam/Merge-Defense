using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSpeedManager : MonoBehaviour
{
    public TMP_Text speedValueText;
    private float[] speeds = { 1f, 2f, 3f };
    private int currentIndex = 0;

    private void Start()
    {
        UpdateSpeed();
    }

    public void ToggleSpeed()
    {
        currentIndex = (currentIndex + 1) % speeds.Length;
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        Time.timeScale = speeds[currentIndex];
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        speedValueText.text = $"X{speeds[currentIndex]}";
    }
}
