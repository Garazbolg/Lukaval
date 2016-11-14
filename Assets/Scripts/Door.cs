using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public Animator anim;

	void OnTriggerEnter()
    {
        Debug.Log("lol");
        anim.SetBool("Open", true);
    }
}
