using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Build : MonoBehaviour, IBrain
{
    private Entity entity;
    protected List<IState> states = new List<IState>();


    public bool Condition() =>
        entity.currBrain == EntityBrains.gather; // Current Enum Brain of Entity


    public Brain_Build(Entity entity) =>
        this.entity = entity; // Get Entity


    /// <summary>
    /// adding needed states for each Entity
    /// </summary>
    public void AddState()
    {//Add states to BaseEntity
        //states.Add(new IFarming(entity));
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
