using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimit : MonoBehaviour
{
    public enum limits {
        NO_LIMIT = 0,
        LIMIT_30FPS = 30,
        LIMIT_60FPS = 60,
        LIMIT_120FPS = 120,
        LIMIT_240FPS = 240,
    }
    public limits limit;

    private void Awake() {
        Application.targetFrameRate = (int) limit;
    }
}
