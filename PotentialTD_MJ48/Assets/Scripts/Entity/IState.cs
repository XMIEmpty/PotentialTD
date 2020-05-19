using System.Collections;
using System.Collections.Generic;

//Michael Schmidt

public interface IState
{
    /// <summary>
    /// Check condition from each state
    /// </summary>
    /// <returns></returns>
    bool Condition();


    /// <summary>
    /// Execute state which condition is true
    /// </summary>
    void Execute();
}
