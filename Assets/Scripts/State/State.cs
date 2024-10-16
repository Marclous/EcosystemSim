using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    
    public abstract bool EnterState();

    public abstract void UpdateState();
    
    public abstract bool ExitState();
}
