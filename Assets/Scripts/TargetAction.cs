using System;
using UnityEngine;

public class TargetAction : MonoBehaviour{
	private Camera playerCam;
	private LayerMask mask = LayerMask.NameToLayer("Activator");


	public float range = 2;
	public GameObject interactUI;

	void Start(){
		playerCam = Camera.main;
	}

	void Update(){
		RaycastHit hit;
		if(Physics.Raycast(playerCam.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.heigth/2,playerCam.nearClipPlane)),playerCam.transform.forward,out hit,range,mask)){
			if(Input.GetButtonDown("Action")){
				Activator act = hit.collider.GetComponent<Activator>();
				if(act)
					act.Activate();
			}
			else{
				interactUI.SetActive(true);
			}
		}
		else{
			interactUI.SetActive(false);
		}
	}
}
