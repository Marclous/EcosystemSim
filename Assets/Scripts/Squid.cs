using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Fish
{
    // Start is called before the first frame update
    public SquidScriptableObject squidData;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform launchPoint; // A point from where the projectile will be launched
    void Start()
    {
        hitPoints = squidData.hitPoint;
        lifespan = squidData.lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        squidData.SquidMovement();
    }

    public override void Attack(Organism opponent) {
        Debug.Log("SquidAttacks");

        // Instantiate the projectile at the launch point
        GameObject projectile = GameObject.Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();
        
        // Set the target for the projectile
        projScript.SetTarget(opponent.transform.position);
    }
}
