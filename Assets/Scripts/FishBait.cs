using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBait : Organism
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Implement collision logic, e.g., damage the target
        Debug.Log(collision.gameObject.name+"got hit");
        Organism target = collision.GetComponent<Organism>();
        if (target != null && target.gameObject.name == "PufferFish")
        {
            Destroy(gameObject); // Destroy the projectile after hitting the target
        }
    }
}
