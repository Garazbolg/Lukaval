using UnityEngine;
using System.Collections;

public class SwapMap : MonoBehaviour {
    public GameObject toKillScene;
    public GameObject nextScene;
    public GameObject lightForAttic;

    void OnTriggerEnter(Collider other)
    {
        toKillScene.SetActive(false);
        nextScene.SetActive(true);
        lightForAttic.SetActive(nextScene.name.Equals("Grenier"));
    }
}
