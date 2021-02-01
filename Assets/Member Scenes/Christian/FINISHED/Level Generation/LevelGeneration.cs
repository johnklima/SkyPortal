using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public List<GameObject> allIslands = new List<GameObject>();
    
    private void Awake()
    {
        foreach(GameObject island in Resources.LoadAll("Islands", typeof(GameObject)))
        {
            allIslands.Add(island);
        }
    }

    void Start()
    {
        int rand = Random.Range(0, allIslands.Count);
        Instantiate(allIslands[rand], transform.position, transform.rotation);
        
    }
    
}
