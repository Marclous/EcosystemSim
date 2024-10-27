using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : Fish
{
    public float enlargeScale = 2.0f; // How much bigger the puffer fish should get
    public float enlargeDuration = 5.0f; // Duration to stay enlarged
    private Vector3 originalScale; // Store the original size

    public int damageAmount = 20;
    private bool isEnlarged = false;
    public PufferFishScriptableObject PufferFishData;
    void Start()
    {
        hitPoints = PufferFishData.hitPoint;
        lifespan = PufferFishData.lifespan;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        base.Update(); //parent Organism function
        
        //anything else specifically in Dory I can write here
    }

    public override void Attack(Organism opponent)
    {
        base.Attack(opponent);
    }

    public IEnumerator Defend() {
        Debug.Log("Puffer fish enlarges");
        isEnlarged = true;

        // Increase the size of the puffer fish
        transform.localScale = originalScale * enlargeScale;
        Debug.Log("Puffer fish enlarged to: " + transform.localScale);

        // Wait for the specified duration
        yield return new WaitForSeconds(enlargeDuration);

        // Return to the original size
        transform.localScale = originalScale;

        // Mark as no longer enlarged
        isEnlarged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the puffer fish is enlarged and colliding with another organism
        if (isEnlarged)
        {
            Organism otherOrganism = collision.GetComponent<Organism>();
            if (otherOrganism != null)
            {
                // Deal damage to the other organism
                otherOrganism.TakeDamage(damageAmount);
                Debug.Log("Puffer fish dealt " + damageAmount + " damage to " + collision.gameObject.name);
            }
        }else if(collision.gameObject.name == "SquidProjectile"){
            Debug.Log("Pufferfish got hit");
            if (!isEnlarged)
        {
            // Trigger the enlarge behavior
            StartCoroutine(Defend());
        }
        }
    }
}
