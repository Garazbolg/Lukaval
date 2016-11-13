using UnityEngine;
using System.Collections;

public class FlickeringHandler : MonoBehaviour {
    public FlickeringLight[] lights;
    public MeshRenderer[] lightsMesh;

    void Update()
    {
        foreach(MeshRenderer r in lightsMesh)
        {
            if(r.enabled != lights[0].spotlight.enabled)
            {
                r.enabled = lights[0].spotlight.enabled;
            }
        }
    }

	void OnTriggerEnter()
    {
        foreach(FlickeringLight l in lights)
        {
            l.ActivateFlickering();
        }
    }
}
