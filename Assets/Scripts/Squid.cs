using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Fish
{
    // Start is called before the first frame update
    public SharkScriptableObject sharkData;

    void Start()
    {
        hitPoints = sharkData.hitPoint;
        lifespan = sharkData.lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        sharkData.SharkMovement();
    }
}
