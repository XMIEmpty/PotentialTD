using UnityEngine;

public class Gate : MonoBehaviour, IPassMethods
{
    public string PassMethodName(int methodNum)
    {
        //ToggleGate();

        switch (methodNum)
        {
            case 0:
                return nameof(ToggleGate);
            case 1:
                return nameof(Rotate);
            case 2: 
                return "";
            case 3: 
                return "";
        }

        return null;
    }


    public string GetScriptName()
    {
        return this.GetType().Name;
    }

    
    public bool isOpen = false;
    private A_Building m_ABuilding;
    private static readonly int CloseGate = Animator.StringToHash("CloseGate");
    private static readonly int OpenGate = Animator.StringToHash("OpenGate");


    private void Start()
    {
        if (!TryGetComponent<A_Building>(out var buildingFound)) return;
        m_ABuilding = buildingFound;
    }


    private void Update() // Use to run constant processes etc. that have been activated or changed based on actions taken etc.
    { 
        //TODO: If this condition case is true do this process etc.
    }


    // Toggles Gate to Open or Closed
    public void ToggleGate()
    {
        if (isOpen)
        {
            // Close gate
            m_ABuilding.animator.SetTrigger(CloseGate);
            Debug.Log("Gate Closed");
        }
        else
        {
            // Open Gate
            m_ABuilding.animator.SetTrigger(OpenGate);
            Debug.Log("Gate Opened");
        }

        isOpen = !isOpen;
    }

    public void Rotate()
    {
        transform.Rotate(transform.rotation.z >= 90f ? Vector3.zero : new Vector3(0, 0f, 90f));
    }

}