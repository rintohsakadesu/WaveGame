using UnityEngine;

public class Scorer : MonoBehaviour
{
   int score = 0;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Target")
        {
            int points = collision.gameObject.GetComponent<Targets>().GetPoints();
            score += points;
        }
        Debug.Log(score);
    }
}
