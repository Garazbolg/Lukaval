using UnityEngine;
using System.Collections;

public class MovingLight : MonoBehaviour {
    public float speed = 0.1f;
    public Transform popSpot;

    public GameObject audioSource;

    void Start()
    {
        if(audioSource)
            AkSoundEngine.PostEvent("NoteMetro",audioSource);
    }

	void Update ()
    {
        transform.position -= speed * transform.forward * Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = popSpot.position;
            if (audioSource)
                AkSoundEngine.PostEvent("NoteMetro", audioSource);
        }
        else if (other.gameObject.CompareTag("WeepingAngel"))
        {
            ShowObject(other, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WeepingAngel"))
        {
            ShowObject(other, false);
        }
    }

    private void ShowObject(Collider obj, bool show)
    {
        StoreScale store = obj.GetComponent<StoreScale>();
        store.Show(show);

        /*
        MeshRenderer render = obj.GetComponentInChildren<MeshRenderer>();
        float scale = (show) ? 1 : 0;

        if (render != null)
        {
            render.enabled = show;
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
        */
    }
}
