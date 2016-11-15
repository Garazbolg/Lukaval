using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public Animator anim;
    public GameObject roomToActivate;
    public GameObject pointLight;

    void Start()
    {

    }

	void OnTriggerEnter()
    {
        anim.SetBool("Open", true);
        AkSoundEngine.PostEvent("Room_Clock_Stop", gameObject);
        AkSoundEngine.PostEvent("Room_Door_Open", gameObject);

        if (roomToActivate != null)
        {
            roomToActivate.SetActive(true);

            if(pointLight != null)
                pointLight.SetActive(roomToActivate.name.Equals("Grenier"));
        }
    }

    void OnTriggerExit()
    {
        anim.SetBool("Open", false);
    }

    public void Open()
    {
        anim.SetBool("Open", true);
    }
}
