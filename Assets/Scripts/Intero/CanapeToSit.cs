using UnityEngine;
using System.Collections;

public class CanapeToSit : GameActivator {

    public Vector3 targetPosition;

    public GameObject targetLookAt;

    public float lerpTime = 3f;

    GameObject player;

    public override void OnActivate()
    {
        base.OnActivate();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Collider>().enabled = false;
        
        StartCoroutine(LerpToPosition());
    }

    IEnumerator LerpToPosition()
    {
        float currentTime = Time.deltaTime;
        Vector3 startPosition = player.transform.position;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetLookRotation = Quaternion.FromToRotation(player.transform.forward, (targetLookAt.transform.position - player.transform.position).normalized);
        while (currentTime < lerpTime) {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition,currentTime/lerpTime);
            player.transform.rotation = Quaternion.Slerp(startRotation, targetLookRotation, currentTime / lerpTime);
            yield return null;
            currentTime += Time.deltaTime;
                }
    } 
}
