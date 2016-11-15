using System;
using UnityEngine;

public class ActivatorDestroySelf : GameActivator
{
    public GameObject itemToActivate;

	public override void OnActivate(){
		base.OnActivate();
		DestroyImmediate(gameObject);
        if(itemToActivate)
            itemToActivate.SetActive(true);
	}
}
