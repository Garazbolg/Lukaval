using UnityEngine;
using System.Collections;

public class SpawnCollider : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
            target.SetActive(true);
    }
}
