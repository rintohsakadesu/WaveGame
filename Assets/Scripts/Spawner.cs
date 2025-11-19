using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    

    [SerializeField] List<GameObject> spawnList;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for(int i=0; i<8; i++)
        {
            int j = Random.Range(0,spawnList.Count-1);
            int xpos = Random.Range(-4,4);
            int zpos = Random.Range(-4,4);
            Instantiate(spawnList[j],transform.position+new Vector3(xpos,0,zpos),Quaternion.identity);
        }
    }
}
