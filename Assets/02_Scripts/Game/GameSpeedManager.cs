using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    private float[] speeds = { 1f, 2f, 3f };
    private int currentIndex = 0;

    public void ToggleSpeed()
    {
        currentIndex = (currentIndex + 1) % speeds.Length;
        Time.timeScale = speeds[currentIndex];
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Debug.Log("현재 게임 속도: x" + speeds[currentIndex]);
    }
}
