using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{

    State startingState;
    State currentState;
    
    public Organism thisOrganism;

    public Exploring exploringState;

    // Start is called before the first frame update
    void Start()
    {
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
