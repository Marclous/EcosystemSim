using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;

public class FiniteStateMachine : MonoBehaviour
{

    State startingState;
    State currentState;
    
    public Organism thisOrganism;
    public string targetSpeciesTag;
    private Organism currentTarget;
    public Organism chaseTarget;

    public float distanceThreshold = 5.0f;
    public float battleRadius = 2.0f;

    public Exploring exploringState;
    public Chasing chasingState;
    public Battle battleState;

    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = thisOrganism.GetComponent<Collider2D>();
        
        exploringState = new Exploring(thisOrganism, this);
        chasingState = new Chasing(thisOrganism, chaseTarget, this);
        battleState = new Battle(thisOrganism, chaseTarget, this);

        startingState = exploringState;

        SwitchToState(startingState);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.name + "'s current state is" + currentState);
        Debug.Log(this.name + "'s current target is" + chaseTarget.gameObject.name);
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(targetSpeciesTag);
        
        float closestDistance = Mathf.Infinity;
        Organism closestOrganism = null;

        foreach (GameObject target in potentialTargets)
        {
            Organism organism = target.GetComponent<Organism>();
            if (organism != null && organism != thisOrganism && organism.hitPoints > 0) // Check if the organism is valid and alive
            {
                float distance = Vector2.Distance(thisOrganism.transform.position, organism.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestOrganism = organism;
                }
            }
        }

        // Update the chase target to the closest organism found
        if (closestOrganism != null)
        {
            chaseTarget = closestOrganism;
        }
        else
        {
            // If no valid targets are found, set chaseTarget to null
            chaseTarget = null;
        }

        if (chaseTarget == null)
        {   
            Debug.Log("Chase target is null. Returning to exploring state.");
        }
        else if (chaseTarget.hitPoints <= 0)
        {
            Debug.Log("Chase target is dead. Returning to exploring state.");
        }
        if(currentState != null) {
            currentState.UpdateState();
        }

        if (chaseTarget == null || chaseTarget.hitPoints <= 0)
        {
        // Target is dead, switch back to exploring state
            if (currentState != exploringState)
            {
                SwitchToState(exploringState);
            }
            return; // Exit early to avoid further checks
        }

        float distanceToTarget = Vector2.Distance(thisOrganism.transform.position, chaseTarget.transform.position);

        // Transition to chasing state if the target is within the chasing threshold
        if (distanceToTarget < distanceThreshold && currentState != chasingState && currentState != battleState)
        {
            SwitchToState(chasingState);
        }
        // Transition to battle state if within the battle radius
        else if (distanceToTarget < battleRadius && currentState != battleState)
        {
            SwitchToState(battleState);
        }
        // Transition back to exploring state if the target is out of the chasing radius
        else if (distanceToTarget >= distanceThreshold && currentState != exploringState)
        {
            SwitchToState(exploringState);
        }

    }

    public void SwitchToState (State stateToSwitchTo) {
        Debug.Log("Switch to state" + stateToSwitchTo + "for" + gameObject.name);
        if(currentState != null)
            currentState.ExitState();

        currentState = stateToSwitchTo;

        if(currentState != null)
            currentState.EnterState();
    }


}
