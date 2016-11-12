using System;
using UnityEngine;

public class ActivatorDestroySelf : GameActivator
{
	public override void OnActivate(){
		base.OnActivate();
		DestroyImmediate(gameObject);
	}
}
