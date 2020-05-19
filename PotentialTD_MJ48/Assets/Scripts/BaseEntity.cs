using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : Entity
{
    BrainMachine brain = new BrainMachine();

    void Start()
    {
        //Add states to BaseEntity
        brain.AddBrain(new Brain_Gather(this));
        brain.AddBrain(new Brain_Build(this));
    }

    void Update()
    {   
        //Call functions
        brain.Update();
    }
}
