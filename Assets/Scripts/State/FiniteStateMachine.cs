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
    public Organism chaseTarget;

    public float distanceThreshold = 5.0f;

    public Exploring exploringState;
    public Chasing chasingState;
    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = thisOrganism.GetComponent<Collider2D>();

        exploringState = new Exploring(thisOrganism, this);

        startingState = exploringState;

        SwitchToState(startingState);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null) {
            currentState.UpdateState();
        }

        if(Vector2.Distance(thisOrganism.transform.position, chaseTarget.transform.position) < distanceThreshold) {
            SwitchToState(chasingState);
        }else if(currentState != exploringState){
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
