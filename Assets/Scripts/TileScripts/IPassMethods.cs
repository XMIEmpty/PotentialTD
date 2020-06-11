using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassMethods
{
    string PassMethodName(int methodNum);

    Vector3Int PassMethodCosts(int methodNum);

    string PassMethodInfo(int methodNum);
    
    string GetScriptName();
}