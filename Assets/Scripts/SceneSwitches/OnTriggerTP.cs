using UnityEngine;
using System.Collections;

public class OnTriggerTP : MonoBehaviour {

    public string targetScene;
    public Animator doorToOpen;

    
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
            if (doorToOpen)
                doorToOpen.SetBool("Open", true);
            StartCoroutine(FadeToBlack());
        }
    }

    public float fadeTime = 1.5f;
    private Texture2D fadeTexture;
    private float startTime;

    IEnumerator FadeToBlack()
    {
        fadeTexture = new Texture2D(2,2,TextureFormat.RGBA32,false);
        fadeTexture.LoadRawTextureData(new byte[16] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0});

        startTime = Time.realtimeSinceStartup;
        Time.timeScale = 0;
        while (Time.realtimeSinceStartup - startTime < fadeTime)
        {
            yield return null;
        }
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
    }
    
    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, (Time.realtimeSinceStartup - startTime) / fadeTime);
        if (fadeTexture != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture as Texture);
    }
}
