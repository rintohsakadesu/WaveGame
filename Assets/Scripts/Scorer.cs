using UnityEngine;
using UnityEngine.EventSystems;

public class Scorer : MonoBehaviour
{
    GameObject eventSystem;
    ScoreKeeper myScoreKeeper;
    public void Start()
    {
        eventSystem= GameObject.FindWithTag("EventSystem");
        myScoreKeeper = eventSystem.GetComponent<ScoreKeeper>();
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Target")
        {
            int points = collision.gameObject.GetComponent<Targets>().GetPoints();
            myScoreKeeper.AddPoints(points);
        }
    }
}
