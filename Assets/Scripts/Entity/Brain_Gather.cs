using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Gather : MonoBehaviour, IBrain
{
    private Entity entity;
    protected List<IState> states = new List<IState>();


    public Brain_Gather(Entity entity) =>
        this.entity = entity; // Get Entity


    public bool Condition() =>
        entity.currBrain == EntityBrains.gather; // Current Enum Brain of Entity


    public void AddState()
    {//Add states to BaseEntity
        states.Add(new IFarming(entity));
            // TreeChopping
            // SoulsHarvesting
            // anything else that belogns to GATHERING
    }


    public void BUpdate()
    {
        foreach (IState state in states)
        {// Run thru all added states
            if (state.Condition())
            {// Check for every state's condition return
                state.Execute();    // Execute state which condition is true
                break;
            }
        }
    }
}
