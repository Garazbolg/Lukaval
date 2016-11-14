using UnityEngine;
using System.Collections;

public class SwapMap : MonoBehaviour {
    public GameObject toKillScene;
    public Animator doorToClose;
    public GameObject nextScene;
    public GameObject nextDoor;
    public GameObject lightForAttic;

    void OnTriggerEnter(Collider other)
    {
        if(toKillScene != null)
            toKillScene.SetActive(false);

        if (nextScene != null)
        {
            nextScene.SetActive(true);
        }

        if(nextDoor != null)
        {
            nextDoor.SetActive(true);
        }

        if (doorToClose != null)
            doorToClose.SetBool("Open", false);

        
    }
}
