using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : State
{
    Organism owner;
    Organism target;

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
        Debug.Log("Updating Exploring State for" + owner.gameObject.name);
        
    }

}
