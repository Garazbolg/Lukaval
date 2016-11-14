using UnityEngine;
using System.Collections;

public class OnTriggerTP : MonoBehaviour {

    public string targetScene;

    void Start()
    {

    }

    void Update()
    {

    }

	void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<CharacterController>())
        {
            AkSoundEngine.PostEvent("Stop_All", gameObject);
            AkSoundEngine.PostEvent("Room_Door_Open",gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
        }
    }
}
