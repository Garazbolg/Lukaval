using UnityEngine;
using System.Collections;

public class MovingLight : MonoBehaviour {
    public float speed = 0.1f;
    public Transform popSpot;

	void Update ()
    {
        transform.position += speed * transform.right;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = popSpot.position;
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            ShowObject(other, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            ShowObject(other, false);
        }
    }

    private void ShowObject(Collider obj, bool show)
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
