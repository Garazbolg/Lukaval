using UnityEngine;
using System.Collections;

public class ShoundSourceEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Load()
    {
        AkSoundEngine.PostEvent("Room_Gun_Load", gameObject);
    }

    void Shoot()
    {
        AkSoundEngine.PostEvent("Room_Gun_Shot", gameObject);
    }

    void Dead()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Black");
    }
}
