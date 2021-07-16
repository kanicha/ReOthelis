using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    // •Ï”
    int frameCount;
    float prevTime;
    float fps;

    // ‰Šú‰»ˆ—
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
    }
    // XVˆ—
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;
            Debug.Log(fps);

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
