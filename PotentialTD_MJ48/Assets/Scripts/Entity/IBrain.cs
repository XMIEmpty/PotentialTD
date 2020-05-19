using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBrain
{
    /// <summary>
    /// Check condition from each state
    /// </summary>
    /// <returns></returns>
    bool Condition();


    /// <summary>
    /// Add Brains
    /// </summary>
    void AddState();


    /// <summary>
    /// Execute state which condition is true
    /// </summary>
    void Update();
}
