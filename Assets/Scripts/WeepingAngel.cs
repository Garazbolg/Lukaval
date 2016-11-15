using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class WeepingAngel : MonoBehaviour {

    public Transform depart;
    public static Transform Depart;
    private Transform player;
    private float fov;
    private Animator anim;
    private Vector3 origine;

    public float speed = 3f;

    private bool soundActive = false;

    private static bool dead = false;

	// Use this for initialization
	void Start () {
        if (depart && !Depart)
        {
            Depart = depart;
        }
        fov = Camera.main.fieldOfView;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        origine = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Angle(player.forward, transform.position - player.position) > fov*0.7f) // En dehors du champ de vision
        {
            if (!soundActive)
            {
                AkSoundEngine.PostEvent("BabyStart", gameObject);
                soundActive = true;
            }

            Vector3 v = (player.position - transform.position);
            v.y = 0;
            transform.position += v.normalized * Time.deltaTime * speed;
            if (!dead && v.magnitude < 0.5f)
            {
                StartCoroutine(FadeToBlack());
            }
            transform.LookAt(transform.position + v);
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
            if (soundActive)
            {
                AkSoundEngine.PostEvent("BabyStop", gameObject);
                soundActive = false;
            }
        }
	}

    void Reset()
    {
        transform.position = origine;
    }

    public float fadeTime = 1.5f;
    private Texture2D fadeTexture;
    private float startTime;

    IEnumerator FadeToBlack()
    {
        fadeTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        fadeTexture.LoadRawTextureData(new byte[16] { 255, 255, 255, 0, 255, 255, 255, 0, 255, 255, 255, 0, 255, 255, 255, 0 });

        dead = true;
        startTime = Time.realtimeSinceStartup;
        AkSoundEngine.PostEvent("BabyCry_Catch", gameObject);
        Time.timeScale = 0;
        while (Time.realtimeSinceStartup - startTime < fadeTime)
        {
            yield return null;
        }
        Time.timeScale = 1;

        foreach (WeepingAngel wp in GameObject.FindObjectsOfType<WeepingAngel>())
        {
            wp.Reset();
        }
        player.position = Depart.position;
        player.rotation = Depart.rotation;

        dead = false;
        fadeTexture = null;
    }

    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, (Time.realtimeSinceStartup - startTime) / fadeTime);
        if (fadeTexture != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture as Texture);
    }
}
