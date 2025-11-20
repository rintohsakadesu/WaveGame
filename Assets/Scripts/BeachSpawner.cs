using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeachSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnList;
    [SerializeField] int beachLength = 8;

    void Start()
    {
        SpawnBeach();
    }

    void SpawnBeach()
    {
        for(int i=0;i<beachLength;i++)
        {
            //find starting x coordinate depending on length of beach
            int startingX = FindStart();
            int j = UnityEngine.Random.Range(0,spawnList.Count);
            Vector3 spawnPos = new Vector3(startingX+(10*i),0.1f,35);
            Instantiate(spawnList[j],spawnPos,Quaternion.identity);
            
        }
    }

    private int FindStart()
    {
        //-10 for starting on -x axis 
        //10 for centering tiles with city background (tile length) times half the number of tiles used (defined by beachLength)
        int beachStartX = -10*beachLength/2;
        return beachStartX;
    }

}
