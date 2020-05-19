using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableThing : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.LogError("I RUN!");
    }
}
