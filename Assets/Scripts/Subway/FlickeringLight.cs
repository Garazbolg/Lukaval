using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {
    public Light spotlight;
    public SphereCollider trigger;
    public int flickeringInterval;
    public int lightOnDuration;

    private float lastFlickering = 0;

	void Start () {
        LightOn(true);
	}
	
	void Update () {
        if ((Time.realtimeSinceStartup * 1000) - lastFlickering > flickeringInterval)
        {
            lastFlickering = Time.realtimeSinceStartup * 1000;
            LightOn(true);
        }
        else if ((Time.realtimeSinceStartup * 1000) - lastFlickering > lightOnDuration)
        {
            LightOn(false);
        }
	}

    void LightOn(bool lightUp)
    {
        spotlight.enabled = lightUp;
        trigger.radius = (lightUp) ? 0 : 3;
    }

    void OnTriggerEnter(Collider other)
    {
        ShowObject(other, false);
    }

    void OnTriggerExit(Collider other)
    {
        ShowObject(other, true);
    }

    private void ShowObject(Collider obj, bool show)
    {
        if (!obj.gameObject.CompareTag("Player"))
        {
            MeshRenderer render = obj.transform.GetComponent<MeshRenderer>();
            float scale = (show) ? 1 : 0;

            if (render != null)
            {
                render.enabled = show;
                obj.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}
