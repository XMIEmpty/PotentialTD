using System.Collections.Generic;

//Michael Schmidt

public class BrainMachine 
{
    /*
     * A brain is containing the functionality of a State Machine.
     * This Concept is a stacked variation of the basic model of a State Machine
     */


    protected List<IBrain> brains = new List<IBrain>();
    

    /// <summary>
    /// adding needed states for each Entity
    /// </summary>
    /// <param name="newBrain"></param>
    public void AddBrain(IBrain newBrain)
    {
        brains.Add(newBrain);
        newBrain.AddState();
    }


    /// <summary>
    /// Condition and execute function
    /// </summary>
    public void Update()
    {
        //Check which condition in state is true
        foreach (IBrain brain in brains)
        {
            if(brain.Condition())
            {
                //Execute state which condition is true
                brain.BUpdate();
                break;
            }
        }
    }
}
