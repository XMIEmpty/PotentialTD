using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    protected List<IState> states = new List<IState>();


    /// <summary>
    /// adding needed states for each Entity
    /// </summary>
    /// <param name="newState"></param>
    public void AddState(IState newState)
    {
        states.Add(newState);
    }


    /// <summary>
    /// Condition and execute function
    /// </summary>
    public void Update()
    {
        //Check which condition in state is true
        foreach (IState state in states)
        {
            if (state.Condition())
            {
                //Execute state which condition is true
                state.Execute();
                break;
            }
        }
    }
}
