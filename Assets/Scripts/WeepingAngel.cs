using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class WeepingAngel : MonoBehaviour {

    private Transform player;
    private float fov;
    private Animator anim;

    public float speed = 3f;

    private bool soundActive = false;

	// Use this for initialization
	void Start () {
        fov = Camera.main.fieldOfView;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Angle(player.forward, transform.position - player.position) > fov) // En dehors du champ de vision
        {
            if (!soundActive)
            {
                AkSoundEngine.PostEvent("BabyStart", gameObject);
                soundActive = true;
            }

            Vector3 v = (player.position - transform.position);
            v.y = 0;
            transform.position += v.normalized * Time.deltaTime * speed;
            transform.LookAt(transform.position + v);
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
            if (soundActive)
            {
                AkSoundEngine.PostEvent("BabyStop", gameObject);
                soundActive = false;
            }
        }
	}
}
