using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
    public Transform endPos;

    void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<CharacterController>()) return;
        StartCoroutine(Open((endPos.position - transform.position).normalized));
        AkSoundEngine.PostEvent("Metro_Door_Open", gameObject);
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator Open(Vector3 translation)
    {
        float dist = (endPos.position - transform.position).magnitude;
        
        while (dist > (0.1f * translation).magnitude)
        {
            transform.parent.position += 0.1f * translation;
            dist = (endPos.position - transform.position).magnitude;
            yield return null;
        }

    }
}
