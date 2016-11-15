using UnityEngine;
using System.Collections;

public class CanapeToSit : GameActivator {

    public Transform target;

    public GameObject ParticleToDeactivate;

    public GameObject targetLookAt;

    public float lerpTime = 3f;

    public QuestionsHandler handler;

    GameObject player;

    public GameObject yesArrow;

    public GameObject noArrow;

    public override void OnActivate()
    {
        base.OnActivate();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Collider>().enabled = false;
        handler.Activated = true;
        handler.ShowNextMessage();
        yesArrow.SetActive(true);
        noArrow.SetActive(true);

        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().allowMoving = false;

        ParticleToDeactivate.SetActive(false);

        StartCoroutine(LerpToPosition());
    }

    IEnumerator LerpToPosition()
    {
        float currentTime = Time.deltaTime;
        Vector3 startPosition = player.transform.position;
        Quaternion startRotation = player.transform.rotation;
        target.LookAt(targetLookAt.transform);

        while (currentTime < lerpTime) {
            player.transform.position = Vector3.Lerp(startPosition, target.position,currentTime/lerpTime);
            player.transform.rotation = Quaternion.Slerp(startRotation, target.rotation, currentTime / lerpTime);
            yield return null;
            currentTime += Time.deltaTime;
                }
    } 
}
