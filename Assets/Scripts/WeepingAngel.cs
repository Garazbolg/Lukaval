using UnityEngine;
using System.Collections;

public class WeepingAngel : MonoBehaviour {

    private Transform player;
    private float fov;

    public float speed = 3f;

	// Use this for initialization
	void Start () {
        fov = Camera.main.fieldOfView;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Angle(player.forward, transform.position - player.position) > fov) // En dehors du champ de vision
        {
            transform.position += (player.position - transform.position).normalized * Time.deltaTime * speed;
        }
	}
}
