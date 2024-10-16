using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Exploring : State
{
    Organism owner;
    Vector2 targetLocation;
    FiniteStateMachine finiteStateMachine;
    public Exploring(Organism owner, FiniteStateMachine finiteStateMachine) {
        this.owner = owner;
        this.finiteStateMachine = finiteStateMachine;
    }
    public override bool EnterState()
    {
        Debug.Log("Entering Exploring State for" + owner.gameObject.name);
        targetLocation.x = Random.Range(0, Screen.width);
        targetLocation.y = Random.Range(0, Screen.height);

        targetLocation = Camera.main.ScreenToWorldPoint(targetLocation);
        Debug.Log("Entered exploring state for" + owner.gameObject.name + " moving to" + targetLocation);

        return true;
    }

    public override bool ExitState()
    {
        Debug.Log("Exiting Exploring State for" + owner.gameObject.name);
        return true;
    }

    public override void UpdateState()
    {
        Debug.Log("Updating Exploring State for" + owner.gameObject.name);

        if((targetLocation.x > 0 && owner.transform.position.x < targetLocation.x) ||
           (targetLocation.x < 0 && owner.transform.position.x > targetLocation.x) ||
           (targetLocation.y > 0 && owner.transform.position.y < targetLocation.y) ||
           (targetLocation.y > 0 && owner.transform.position.y < targetLocation.y)) {
            owner.transform.position = Vector2.MoveTowards(owner.transform.position,targetLocation, Time.deltaTime);
        }
        else {
            Debug.Log("Time to pick a new location");
            finiteStateMachine.SwitchToState(finiteStateMachine.exploringState);
        }


    }

}
