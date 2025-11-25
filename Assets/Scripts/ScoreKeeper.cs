using UnityEngine;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{
    int myPoints = 0;

    public void AddPoints(int points)
    {
        myPoints +=points;
        Debug.Log(myPoints);
    }

    public void MinusPoints(int points)
    {
        myPoints-=points;
        Debug.Log(myPoints);
    }
    public int GetCurrentPoints()
    {
        return myPoints;
    }

}
