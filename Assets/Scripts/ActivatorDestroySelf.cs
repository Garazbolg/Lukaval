using System;
using UnityEngine;

public class ActivatorDestroySelf : Activator{
	private override void OnActivate(){
		base.OnActivate();
		DestroyImmediate(gameObject);
	}
}
