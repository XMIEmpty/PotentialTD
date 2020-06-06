using UnityEngine;

public class ITreeChopping : MonoBehaviour, IState
{
    private Entity entity;
    
    public ITreeChopping(Entity entity) => this.entity = entity;


    public bool Condition() => 
        entity.currState == EntityStates.MushTreeChopping;


    public void Execute()
    {
        if (entity.returnHarvested == false &&(entity.target == null || entity.target.tag != "MushTree"))
        {
            GetClosestThing("MushTree");
            Debug.Log("New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
        }

        if (entity.target.tag == "MushTree" && entity.returnHarvested == false)
        {
            if (GetTargetDistance() > 1f)
            {
                LookAtTarget();
                Chase();
                Debug.Log("Moving To Tree");
            }

            if (GetTargetDistance() < 1f)
            {
                entity.characterRB.velocity = new Vector2(0.0f, 0.0f);

                if (entity.currWorkTime < entity.target.GetComponent<MushTree_Old>().currHealth &&
                    entity.currWorkTime < entity.maxWorkTime)
                    entity.currWorkTime += Time.deltaTime;
                Debug.Log("Chop!");
                if (entity.currWorkTime >= entity.maxWorkTime ||
                    entity.currWorkTime >= entity.target.GetComponent<MushTree_Old>().currHealth)
                {
                    entity.target.GetComponent<MushTree_Old>().Harvest(Mathf.RoundToInt(entity.currWorkTime));
                    GetClosestThing("Hall");
                    Debug.Log("NNN New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
                    entity.returnHarvested = true;
                }
            }
        }

        if (entity.target.tag == "Hall" && entity.returnHarvested == true)
        { 
            if (GetTargetDistance() > 1f)
            {
                LookAtTarget();
                Chase();
                Debug.Log("Moving To Hall");
            }

            if (GetTargetDistance() < 1f)
            {
                entity.characterRB.velocity = new Vector2(0.0f, 0.0f);
                // Instantiate returned  logs Particle
                
                // entity.target.GetComponent<Hall_Old>().GetMushLogs(Mathf.RoundToInt(entity.currWorkTime));
                entity.currWorkTime = 0;
                entity.target = null;
                entity.returnHarvested = false;
                Debug.Log("Delivered");
            }
            // Harvest Target Tree
        }
    }


    /// <summary>
    /// Moving towards facing direction
    /// </summary>
    private void Chase() =>
        entity.characterRB.velocity = entity.facingDirection.transform.forward * 100 *
            entity.movementSpeed * Time.deltaTime;


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    private void LookAtTarget() =>
        entity.facingDirection.transform.LookAt(entity.target.transform);


    private float GetTargetDistance() => Vector2.Distance(
        entity.transform.position,
        entity.target.transform.position);
    
    
    public void GetClosestThing(string tagToSeekFor)
    {
        Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(entity.transform.position, entity.sightRange);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            if (collisionsInCastArea[i] == GetClosest(collisionsInCastArea, tagToSeekFor))
            {
                entity.target = collisionsInCastArea[i].gameObject;
            }
        }
    }


    private Collider2D GetClosest(Collider2D[] list, string tagToSeekFor)
    {
        Collider2D closestTarget = null;
        float closestDistance = entity.sightRange;
        foreach (Collider2D target in list)
        {
            if (target.name != entity.name && target.tag == tagToSeekFor)
            {
                float distance = Vector2.Distance(entity.transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        return closestTarget;
    }
}
