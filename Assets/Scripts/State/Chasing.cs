using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : State
{
    Organism owner;
    Organism target;
    public float chasingSpeed = 2.0f;
    FiniteStateMachine finiteStateMachine;
    public Chasing(Organism owner, Organism target, FiniteStateMachine finiteStateMachine) {
        this.owner = owner;
        this.target = target;
        this.finiteStateMachine = finiteStateMachine;
    }
    public override bool EnterState()
    {
        Debug.Log("Entering Chasing State for" + owner.gameObject.name);
        owner.transform.position = Vector2.MoveTowards(owner.transform.position, target.transform.position, Time.deltaTime);
        
        return true;
    }

    public override bool ExitState()
    {
        Debug.Log("Exiting Chasing State for" + owner.gameObject.name);
        return true;
    }

    public override void UpdateState()
    {

        Debug.Log("Updating Chasing State for" + owner.gameObject.name);
        if (target == null || target.hitPoints <= 0)
        {
            Debug.Log("Target is destroyed or dead, switching to exploring state.");
            finiteStateMachine.SwitchToState(finiteStateMachine.exploringState);
            return;
        }
        // Check if target still exists and is within chasing range
        float distanceToTarget = Vector2.Distance(owner.transform.position, target.transform.position);
        if (target != null && distanceToTarget < finiteStateMachine.distanceThreshold && target.hitPoints>0) 
        {
            // Move towards the target if it's within the radius
            owner.transform.position = Vector2.MoveTowards(owner.transform.position, target.transform.position, chasingSpeed * Time.deltaTime);
        }
        else 
        {
            // If the target moves out of range, switch to exploring state
            Debug.Log("Target out of range, switching to exploring state.");
            finiteStateMachine.SwitchToState(finiteStateMachine.exploringState);
        }
    }

}
