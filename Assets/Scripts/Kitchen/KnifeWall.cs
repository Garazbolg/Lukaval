using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class KnifeWall : MonoBehaviour{

	public float speed = 3f;

	private Transform[] knifes;
	public float knifeSpeed = 90; //in degrees per seconds

    public Light[] lihts;

    void Start()
    {
        foreach(KitchenFourniture kf in FindObjectsOfType<KitchenFourniture>())
        {
            kf.Reset();
        }
		knifes = GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < knifes.Length; i++)
            knifes[i].gameObject.SetActive(true);

        foreach(Light l in lihts)
        {
            l.intensity = l.intensity * 2;
            l.spotAngle = l.spotAngle * 1.3f;
        }
    }

	void Update(){
		transform.position += transform.forward.normalized * speed * Time.deltaTime;

		for(int i = 1 ; i < knifes.Length;i++){
			knifes[i].Rotate(Vector3.forward,knifeSpeed * Time.deltaTime,Space.Self);
		}
	}

	void OnTriggerEnter(Collider other){
        if (other.GetComponent<CharacterController>())
            StartCoroutine(FadeToBlack());
	}
    
    public float fadeTime = 1.5f;
    private Texture2D fadeTexture;
    private float startTime;

    IEnumerator FadeToBlack()
    {
        fadeTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        fadeTexture.LoadRawTextureData(new byte[16] { 255, 255, 255, 0, 255, 255, 255, 0, 255, 255, 255, 0, 255, 255, 255, 0 });

        startTime = Time.realtimeSinceStartup;
        AkSoundEngine.PostEvent("BabyCry_Catch", gameObject);
        Time.timeScale = 0;
        while (Time.realtimeSinceStartup - startTime < fadeTime)
        {
            yield return null;
        }
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Jeu");
    }

    void OnGUI()
    {
        GUI.color = new Color(1,1, 1, (Time.realtimeSinceStartup - startTime) / fadeTime);
        if (fadeTexture != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture as Texture);
    }
}
