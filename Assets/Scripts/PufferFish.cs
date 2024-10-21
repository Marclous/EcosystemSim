using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dory : Fish
{
    // Start is called before the first frame update
    void Start()
    {
        howLongLivedSoFar = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        base.Update(); //parent Organism function

        //anything else specifically in Dory I can write here
    }
}
