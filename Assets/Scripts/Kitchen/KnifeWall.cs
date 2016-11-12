using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KnifeWall : MonoBehaviour{

	public float speed = 3f;

	void Update(){
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider){
		Debug.Log("Dead");
	}
}
