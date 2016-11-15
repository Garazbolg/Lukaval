using UnityEngine;
using System.Collections;

public class SwapMap : MonoBehaviour {
    public GameObject toKillScene;
    public Animator doorToClose;
    public GameObject nextScene;
    public GameObject nextDoor;
    public GameObject lightForAttic;

    public string SoundEvent = "";

    void OnTriggerEnter(Collider other)
    {
        if (toKillScene != null)
            Destroy(toKillScene);//.SetActive(false);

        if (SoundEvent.CompareTo("") != 0)
            AkSoundEngine.PostEvent(SoundEvent, gameObject);

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
