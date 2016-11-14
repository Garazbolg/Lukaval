using UnityEngine;
using System.Collections;

public class QuestionsHandler : MonoBehaviour {
    public TextMesh Question;
    public bool Activated = false;
    public GameObject toActivate;
    public Door door;

    private string[] m_Questions;
    private int count = 0;

    private int overCount = 0;
    private GameObject player;

    void Start()
    {
        m_Questions = new string[3];
        m_Questions[0] = "Do you have\nany idea\n<color=#840000>where</color> you are ?";
        m_Questions[1] = "Isn't <color=#840000>guilt</color>\nconsuming you ?";
        m_Questions[2] = "Ain't you feeling\nthis <color=#840000>loop</color>\nin your mind ?";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ShowNextMessage()
    {
        Question.text = m_Questions[count];
        count = (count < m_Questions.Length-1) ? count + 1 : count;
        overCount++;

        if(overCount == 6)
        {
            toActivate.SetActive(true);
            StartCoroutine(LerpToPosition());
        }
    }

    IEnumerator LerpToPosition()
    {
        float currentTime = Time.deltaTime;
        Vector3 startPosition = player.transform.position;
        Vector3 target = player.transform.position + player.transform.up/3;

        while (currentTime < 0.5f)
        {
            player.transform.position = Vector3.Lerp(startPosition, target, currentTime / 0.5f);
            yield return null;
            currentTime += Time.deltaTime;
        }

        Question.text = "";
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().allowMoving = true;
        door.Open();
    }
}
