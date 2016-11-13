using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {
    public Light spotlight;

    private bool activated = false;
    private float lastFlickering = 0;
    private float[] sequenceTimes;
    private int sequenceStep = 0;

    void Start()
    {
        sequenceTimes = new float[7];
        sequenceTimes[0] = 500;
        sequenceTimes[1] = 300;
        sequenceTimes[2] = 700;
        sequenceTimes[3] = 200;
        sequenceTimes[4] = 100;
        sequenceTimes[5] = 200;
        sequenceTimes[6] = 100;
    }
	
	void Update () {
        if (activated)
        {
            if (sequenceStep < sequenceTimes.Length && (Time.realtimeSinceStartup * 1000) - lastFlickering > sequenceTimes[sequenceStep])
            {
                spotlight.enabled = !spotlight.enabled;
                sequenceStep++;
                lastFlickering = Time.realtimeSinceStartup * 1000;
            }
            else if (sequenceStep == sequenceTimes.Length && (Time.realtimeSinceStartup * 1000) - lastFlickering > sequenceTimes[sequenceStep-1])
            {
                spotlight.enabled = false;
                AkSoundEngine.PostEvent("Metro_Flicker_Stop", gameObject);
                sequenceStep++;
            }
        }
	}

    public void ActivateFlickering()
    {
        if (!activated)
        {
            activated = true;
            spotlight.enabled = false;
            lastFlickering = Time.realtimeSinceStartup * 1000;
            AkSoundEngine.PostEvent("Metro_Flicker", gameObject);
        }
        
    }
}
