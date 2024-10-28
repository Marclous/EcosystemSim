using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuna : Fish
{
    public float dashSpeed = 10.0f;   // Speed at which the tuna dashes
    public float dashDistanceBehind = 2.0f; // Distance of each dash back and forth
    public int damageAmount = 10;     // Amount of damage dealt to the target organism
    public float attackCooldown = 1.0f; // Cooldown time between attacks

    FiniteStateMachine finiteStateMachine;
    public TunaScriptableObject tunaData;
    private bool isDashing = false;
    private bool movingForward = true;
    private float cooldownTimer = 0.0f;
     
    private Organism currentTarget;
    private Vector2 finalPosition;
    void Start()
    {
        lifespan = tunaData.lifespan;
        hitPoints = tunaData.hitPoint;
        finiteStateMachine = GetComponent<FiniteStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        /*if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Only perform dashing if not on cooldown
        /*if (cooldownTimer <= 0)
        {
            Attack(currentTarget);
        }*/
        if (!isDashing)
        {
            // Ensure that there is a target to dash towards
            if (currentTarget != null)
            {
                PrepareDashThroughTarget();
            }
        }
        // If the tuna is actively dashing
        if (isDashing)
        {
            DashThroughTarget();
        }
    }

    private void PrepareDashThroughTarget()
    {
        if (currentTarget == null) return;

        // Calculate the direction from the tuna to the target
        Vector2 directionToTarget = (currentTarget.transform.position - transform.position).normalized;

        // Set the final position behind the target based on the specified distance
        finalPosition = (Vector2)currentTarget.transform.position + (directionToTarget * dashDistanceBehind);

        // Start the dash
        isDashing = true;
    }
    public override void Attack(Organism opponent)
    {
        currentTarget = opponent;
        base.Attack(opponent);

    }
    // Dash towards the final position and deal damage
    private void DashThroughTarget()
    {
        // Move towards the final position behind the target
        transform.position = Vector2.MoveTowards(transform.position, finalPosition, dashSpeed * Time.deltaTime);

        // Check if the tuna has reached the final position
        if (Vector2.Distance(transform.position, finalPosition) <= 0.1f)
        {
            // End the dash and apply cooldown
            isDashing = false;
            cooldownTimer = attackCooldown;

            // If the target is still alive, dash again (can be improved to re-check or search for a new target)
            if (currentTarget != null && currentTarget.hitPoints > 0)
            {
                // Prepare the next dash
                PrepareDashThroughTarget();
            }else {
                finiteStateMachine.chaseTarget = null;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the tuna collided with a target organism while dashing
        Debug.Log(collision.gameObject.name+"got hit");
        Organism target = collision.GetComponent<Organism>();
        if (target != null )
        {
            target.TakeDamage(damageAmount);
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Visualize the final position in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(finalPosition, 0.2f);
    }
}
