using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fpsCountScript : MonoBehaviour
{

    public int frameRate;
    public Text fpsCountDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        frameRate = (int)current;
        fpsCountDisplay.text = frameRate.ToString() + " FPS";
    }
}