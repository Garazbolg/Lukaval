using System;
using UnityEngine;

public class TargetAction : MonoBehaviour{
	private Camera playerCam;
    private LayerMask mask;

    public GameObject pausePrefab;

    public float range = 2;
	public GameObject interactUI;

    private Renderer lastRenderer = null;
    private Material lastMaterial = null;

    public Material outlineMaterial;

	void Start(){
		playerCam = Camera.main;
        mask = 1 << LayerMask.NameToLayer("Activator");
    }

	void Update(){
        if (Input.GetButtonDown("Pause") && Time.timeScale > 0.5)
        {
            Instantiate(pausePrefab);
        }

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

                if (act)
                {
                    if (!act.GetComponent<ActivatorGun>())
                        AkSoundEngine.PostEvent("Pick_Up", gameObject);
                    else
                        AkSoundEngine.PostEvent("Room_Gun_Pick_Up", gameObject);
                    act.Activate();
                }
            }
			else{
				interactUI.SetActive(true);
			}
		}
		else{
			interactUI.SetActive(false);
            if (lastRenderer)
            {
                lastRenderer.material = lastMaterial;
            }
            lastRenderer = null;
            lastMaterial = null;
        }
	}

    void OnGUI()
    {
        //GUI.Label()
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width / 2 - 5, Screen.height / 2-3, 20, 20),"+");
    }
}
