using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Build : MonoBehaviour, IBrain
{
    private Entity entity;
    protected List<IState> states = new List<IState>();


    public Brain_Build(Entity entity) =>
        this.entity = entity; // Get Entity


    public bool Condition() =>
        entity.currBrain == EntityBrains.build; // Current Enum Brain of Entity


    public void AddState()
    {}


    public void BUpdate()
    {
        foreach (IState state in states)
        { //Run thru all added states
            if (state.Condition())
            {// Check for every state's condition return
                state.Execute();    // Execute state which condition is true
                break;
            }
        }
    }
}
