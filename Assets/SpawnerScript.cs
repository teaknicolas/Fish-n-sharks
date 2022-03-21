using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //<

    public GameObject Shark;

    public float cycleTimer = 3.0f;
    float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = cycleTimer;
    }
  
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(currentTime > 0)
        {
            currentTime = currentTime -  Time.deltaTime;
            
        }
        else
        {
            Instantiate(Shark, this.transform);
            currentTime = cycleTimer;
        }
    }
}
