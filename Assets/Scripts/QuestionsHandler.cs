using UnityEngine;
using System.Collections;

public class QuestionsHandler : MonoBehaviour {
    public TextMesh Question;
    public bool Activated = false;
    public GameObject toActivate;

    private string[] m_Questions;
    private int count = 0;

    private int overCount = 0;

    void Start()
    {
        m_Questions = new string[3];
        m_Questions[0] = "Do you have\nany idea\n<color=#840000>where</color> you are ?";
        m_Questions[1] = "Isn't <color=#840000>guilt</color>\nconsuming you ?";
        m_Questions[2] = "Ain't you feeling\nthis <color=#840000>loop</color>\nin your mind ?";
    }

    public void ShowNextMessage()
    {
        Question.text = m_Questions[count];
        count = (count < m_Questions.Length-1) ? count + 1 : count;
        overCount++;

        Debug.Log(overCount);
        if(overCount == 6)
        {
            //Send back to room
            //Fade out
            toActivate.SetActive(true);
        }
    }
}
