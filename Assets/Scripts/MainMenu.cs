using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string targetScene;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
    }

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            Play();
        }
    }
}
