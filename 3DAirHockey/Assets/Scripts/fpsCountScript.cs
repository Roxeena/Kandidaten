using UnityEngine;
using UnityEngine.UI;

/* Author: 
 * Last change date: 
 * Checked by: Malin Ejdbo
 * Date of check: 2018-03-13
 * Comment: Good documentation.
*/

// Calculates frames/second over each updateInterval instead of "1f / Time.unscaledDeltaTime",
// so the display does not keep changing all the time.

// Fairly accurate at very low FPS counts (<10).
// This is done by not simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like 5.5 frames.
public class fpsCountScript : MonoBehaviour
{
    public float updateInterval = 0.5F;
    public Text fpsCountDisplay;
    private float accum = 0;            // FPS accumulated over the interval
    private int frames = 0;             // Frames drawn over the interval
    private float timeleft;             // Left time for current interval

    void Start()
    {
        //if (!fpsCountDisplay)
        //{
        //    Debug.Log("Det behövs en GUIText komponent!");
        //    enabled = false;
        //    return;
        //}
        //timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0)
        {
            float fps = (int)accum / frames;
             fpsCountDisplay.text = fps + " FPS";

            if (fps < 30)
                fpsCountDisplay.material.color = Color.yellow;
            else
                if (fps < 10)
                fpsCountDisplay.material.color = Color.red;
            else
                fpsCountDisplay.material.color = Color.green;
            //	DebugConsole.Log(format,level);

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}