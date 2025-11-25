using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnList;

    List<Vector3> spawnPoints = new List<Vector3>();

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for(int i=0; i<8; i++)
        {
            int j = Random.Range(0,spawnList.Count);
            Vector3 newSpawn = ChooseLocation();
            Instantiate(spawnList[j],transform.position+ChooseLocation(),Quaternion.identity);

        }
    }

    Vector3 ChooseLocation()
    {
        bool newSpawn = false;
        Vector3 spawn = new Vector3(0,0,0);
        while(newSpawn==false)
        {
            int xpos = Random.Range(-4,4);
            int zpos = Random.Range(-4,4);
            spawn = new Vector3(xpos,0,zpos);
            if(!spawnPoints.Contains(spawn))
            {
                newSpawn=true;
            }
    
        }
        
        spawnPoints.Add(spawn);
        return spawn;
    }
}
