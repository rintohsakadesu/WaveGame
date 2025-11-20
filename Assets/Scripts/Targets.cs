using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [SerializeField] int points = 100;
    bool pointsAdded = false;
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Wave")
        {
            Destroy(this.gameObject,5);
        }
    }
    public int GetPoints()
    {
        if(pointsAdded==false)
        {
            pointsAdded=true;
            return points;
        }
        else return 0;
        
    }
}
