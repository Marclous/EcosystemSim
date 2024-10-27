using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : State
{
    Organism owner;
    Organism opponent;
    FiniteStateMachine finiteStateMachine;
    
    
    public int attackDamage = 10; // Example damage per attack
    public float attackInterval = 1.0f; // Time between attacks
    private float attackTimer;

    public Battle(Organism owner, Organism opponent, FiniteStateMachine finiteStateMachine) 
    {
        this.owner = owner;
        this.opponent = opponent;
        this.finiteStateMachine = finiteStateMachine;
    }

    public override bool EnterState()
    {
        Debug.Log("Entering Battle State between " + owner.gameObject.name + " and " + opponent.gameObject.name);
        attackTimer = 0.0f;
        return true;
    }

    public override bool ExitState()
    {
        Debug.Log("Exiting Battle State for " + owner.gameObject.name);
        return true;
    }

    public override void UpdateState()
    {
        Debug.Log("Updating Battle State for " + owner.gameObject.name);

        // Check if both organisms are still close enough to battle
        float distanceToOpponent = Vector2.Distance(owner.transform.position, opponent.transform.position);
        if (distanceToOpponent > finiteStateMachine.distanceThreshold)
        {
            // If the opponent is out of range, stop the battle
            Debug.Log("Opponent moved out of range, exiting battle state.");
            finiteStateMachine.SwitchToState(finiteStateMachine.exploringState);
            return;
        }

        // Handle attack logic
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            owner.Attack(opponent);
            attackTimer = 0.0f;
        }
    }

    public void Attack()
    {
       
    
    }
}
