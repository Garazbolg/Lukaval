using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KnifeWall : MonoBehaviour{

	public float speed = 3f;

    void Start()
    {
        foreach(KitchenFourniture kf in FindObjectsOfType<KitchenFourniture>())
        {
            kf.Reset();
        }

    }

	void Update(){
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		Debug.Log("Dead");
	}
}
