using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameActivator : MonoBehaviour{
	public MonoBehaviour targetToActivate;

	public virtual void Start(){
		gameObject.layer = LayerMask.NameToLayer("Activator");
	}

	public virtual void OnActivate(){
		if(targetToActivate)
            targetToActivate.enabled = true;
	}

	public void Activate(){
		OnActivate();
	}
}
