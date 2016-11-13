using UnityEngine;
using System.Collections;

public class StoreScale : MonoBehaviour {
    public MeshRenderer[] renders;

    private Vector3 originScale;

    public string soundOnAppear;

    public string soundOnDisappear;

    void Start ()
    {
        originScale = transform.localScale;
	}
	
	public void Show(bool show)
    {
        if (show)
        {
            AkSoundEngine.PostEvent(soundOnAppear, gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent(soundOnDisappear, gameObject);
            AkSoundEngine.PostEvent("BabyDesappear", gameObject);
        }

        foreach(MeshRenderer r in renders)
        {
            r.enabled = show;
        }

        if(show && !transform.localScale.Equals(originScale))
        {
            transform.localScale = originScale;
        }
        else if(!show && transform.localScale.Equals(originScale))
        {
            transform.localScale = Vector3.zero;
        }
    }
}
