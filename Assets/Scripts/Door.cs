using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public Animator anim;
    public GameObject roomToActivate;
    public GameObject pointLight;

	void OnTriggerEnter()
    {
        anim.SetBool("Open", true);

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
