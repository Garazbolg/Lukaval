using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KnifeWall : MonoBehaviour{

	public float speed = 3f;

	private Transform[] knifes;
	public float knifeSpeed = 90; //in degrees per seconds

    void Start()
    {
        foreach(KitchenFourniture kf in FindObjectsOfType<KitchenFourniture>())
        {
            kf.Reset();
        }
		knifes = GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < knifes.Length; i++)
            knifes[i].gameObject.SetActive(true);

    }

	void Update(){
		transform.position += transform.forward.normalized * speed * Time.deltaTime;

		for(int i = 1 ; i < knifes.Length;i++){
			knifes[i].Rotate(Vector3.forward,knifeSpeed * Time.deltaTime,Space.Self);
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log("Dead");
	}
}
