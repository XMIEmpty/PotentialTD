using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateMethods
{
    /// <summary>
    /// Makes a list of 2D_Collisions found around this enitity at the radius of this entity's sightRange
    /// <para>Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list</para>
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns>Closest Game Object Found</returns>
    public GameObject GetClosestThing(Entity entity, string tagToSeekFor)
    {
        if (entity.target != null) entity.lastTarget = entity.target;

        Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(entity.transform.position, entity.sightRange);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            if (collisionsInCastArea[i] == GetClosest(entity, collisionsInCastArea, tagToSeekFor))
            {
                return collisionsInCastArea[i].gameObject;
                //Debug.Log("New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
            }
        }
        return null;
    }



    /// <summary>
    /// Makes a list of 2D_Collisions found around this enitity at the radius of this entity's sightRange
    /// <para>Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list</para>
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns>Closest Game Object Found</returns>
    public GameObject GetClosestThing(Entity entity, int idToSeekFor)
    {
        if (entity.target != null) entity.lastTarget = entity.target;

        Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(entity.transform.position, entity.sightRange);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            if (collisionsInCastArea[i] == GetClosest(entity, collisionsInCastArea, idToSeekFor))
            {
                return collisionsInCastArea[i].gameObject;
                //Debug.Log("New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
            }
        }
        return null;
    }


    /// <summary>
    /// Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="list">list of 2D_Collisions made</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns></returns>
    private Collider2D GetClosest(Entity entity, Collider2D[] list, string tagToSeekFor)
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


    /// <summary>
    /// Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="list">list of 2D_Collisions made</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns></returns>
    private Collider2D GetClosest(Entity entity, Collider2D[] list, int idToSeekFor)
    {
        Collider2D closestTarget = null;
        float closestDistance = entity.sightRange;

        foreach (Collider2D target in list)
        {
            if (target.name != entity.name && target.GetInstanceID() == idToSeekFor)
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


    /// <summary>
    /// Makes a list of 2D_Collisions found around this enitity at the radius of this entity's sightRange
    /// <para>Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list</para>
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns>Closest Game Object Found</returns>
    public GameObject GetClosestThingAvailable(Entity entity, string tagToSeekFor)
    {
        if (entity.target != null) entity.lastTarget = entity.target;

        Collider2D[] collisionsInCastArea = Physics2D.OverlapCircleAll(entity.transform.position, entity.sightRange);

        for (int i = 0; i < collisionsInCastArea.Length; i++)
        {
            if (collisionsInCastArea[i] == GetClosestAvailable(entity, collisionsInCastArea, tagToSeekFor))
            {
                return collisionsInCastArea[i].gameObject;
                //Debug.Log("New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
            }
        }
        return null;
    }


    /// <summary>
    /// Finds the closest to this Entity 2D_Collision in the inputed 2D_Collider list
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <param name="list">list of 2D_Collisions made</param>
    /// <param name="tagToSeekFor">how is the desired 2D_Collision Tagged</param>
    /// <returns></returns>
    private Collider2D GetClosestAvailable(Entity entity, Collider2D[] list, string tagToSeekFor)
    {
        Collider2D closestTarget = null;
        float closestDistance = entity.sightRange;

        foreach (Collider2D target in list)
        {
            if (target.name != entity.name && target.tag == tagToSeekFor)
            {
                float distance = Vector2.Distance(entity.transform.position, target.transform.position);

                if (distance < closestDistance && target.gameObject.layer == LayerMask.NameToLayer("Available"))
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        return closestTarget;
    }


    public GameObject GetClosestFarm(Entity entity, string targetObject)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < entity.buildings.farms.Count; i++)
        {
            for (int j = 0; j < entity.buildings.farms[i].lands.Count; j++)
            {
                if (entity.buildings.farms[i].lands[j].gameObject.layer == LayerMask.NameToLayer("Available"))
                {
                    float distance = Vector2.Distance(entity.transform.position,entity.buildings.farms[i].lands[j].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = entity.buildings.farms[i].lands[j].gameObject;
                    }
                }
            }
        }
        return closestTarget;
    }


    /// <summary>
    /// Moving towards facing direction
    /// </summary>
    /// <param name="entity">this entity</param>
    public void Chase(Entity entity) =>
        entity.characterRB.velocity = entity.facingDirection.transform.forward * 100 *
            entity.movementSpeed * Time.deltaTime;


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    /// <param name="entity">this entity</param>
    public void LookAtTarget(Entity entity) =>
        entity.facingDirection.transform.LookAt(entity.target.transform);


    /// <summary>
    /// Set Entity's facing direction towards Target position
    /// </summary>
    /// <param name="entity">this entity</param>
    public void LookAtLastTarget(Entity entity) =>
        entity.facingDirection.transform.LookAt(entity.lastTarget.transform);


    /// <summary>
    /// Get Distance between this Entity and this entity's target
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <returns></returns>
    public float GetTargetDistance(Entity entity) => Vector2.Distance(
        entity.transform.position,
        entity.target.transform.position);


    /// <summary>
    /// Get Distance between this Entity and this entity's target
    /// </summary>
    /// <param name="entity">this entity</param>
    /// <returns></returns>
    public float GetLastTargetDistance(Entity entity) => Vector2.Distance(
        entity.transform.position,
        entity.lastTarget.transform.position);
}
