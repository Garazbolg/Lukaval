using UnityEngine;
using System.Collections;

public class PauseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0f;
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        Cursor.visible = true;
	}

    public void Credit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        Cursor.visible = false;
        GameObject.DestroyImmediate(gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Resume();
        }
    }
}
