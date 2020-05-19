using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBrain
{
    /// <summary>
    /// Brain's condition return true at BrainMachine.cs
    /// </summary>
    /// <returns></returns>
    bool Condition();


    /// <summary>
    /// States to add in this brain
    /// <para>Example (for Farming State): <para>states.Add(new IFarming(entity));</para></para>
    /// </summary>
    void AddState();


    /// <summary>
    /// Execute state which condition is true
    /// </summary>
    void BUpdate();
}
