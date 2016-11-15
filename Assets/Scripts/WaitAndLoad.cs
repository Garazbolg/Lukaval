using UnityEngine;
using System.Collections;

public class WaitAndLoad : MonoBehaviour
{

    private float startTima;
    public string target = "MainMenu";
    public float waitTime = 5f;

    // Use this for initialization
    void Start()
    {
        startTima = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - startTima > waitTime)
            UnityEngine.SceneManagement.SceneManager.LoadScene(target);
    }
}