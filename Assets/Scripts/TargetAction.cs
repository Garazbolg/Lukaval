using System;
using UnityEngine;

public class TargetAction : MonoBehaviour{
	private Camera playerCam;
    private LayerMask mask;


	public float range = 2;
	//public GameObject interactUI;

    private Renderer lastRenderer = null;
    private Material lastMaterial = null;

    public Material outlineMaterial;

	void Start(){
		playerCam = Camera.main;
        mask = 1 << LayerMask.NameToLayer("Activator");
    }

	void Update(){
		RaycastHit hit;
		if(Physics.Raycast(playerCam.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height/2,playerCam.nearClipPlane)),playerCam.transform.forward,out hit,range,mask) && hit.collider.GetComponent<GameActivator>())
        {
            Renderer newRen = hit.collider.GetComponent<Renderer>();
            if(newRen != lastRenderer)
            {
                if (lastRenderer)
                {
                    lastRenderer.material = lastMaterial;
                }
                lastMaterial = newRen.material;
                newRen.material = outlineMaterial;
                lastRenderer = newRen;
            }

			if(Input.GetButtonDown("Action")){
                GameActivator act = hit.collider.GetComponent<GameActivator>();
                AkSoundEngine.PostEvent("Pick_Up",gameObject);
				if(act)
					act.Activate();
			}
			else{
				//interactUI.SetActive(true);
			}
		}
		else{
			//interactUI.SetActive(false);
            if (lastRenderer)
            {
                lastRenderer.material = lastMaterial;
            }
            lastRenderer = null;
            lastMaterial = null;
        }
	}
}
