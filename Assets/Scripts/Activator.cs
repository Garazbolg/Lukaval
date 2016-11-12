using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Activator : MonoBehaviour{
	public Component targetToActivate;

	public virtual void Start(){
		gameObject.layer = LayerMask.NameToLayer("Activator");
	}

	private virtual void OnActivate(){
		targetToActivate.enabled = true;
	}

	public void Activate(){
		OnActivate();
	}
}
