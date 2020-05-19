using System;
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


    public Harvestable GetClosestHarvestable(Entity entity, string plantTag)
    {
        Harvestable closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < entity.buildings.allHarvestables.Count; i++)
        {
            if (entity.buildings.allHarvestables[i].plantTag == plantTag &&
                entity.buildings.allHarvestables[i].isAvailable == true)
            {
                float distance = Vector2.Distance(entity.transform.position, entity.buildings.allHarvestables[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = entity.buildings.allHarvestables[i];
                }
            }
        }

        return closestTarget;
    }

    public A_Building GetClosestBuilding(Entity entity, string buildingTag)
    {
        A_Building closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < entity.buildings.allBuildings.Count; i++)
        {
            if (entity.buildings.allBuildings[i].buildingTag == buildingTag)
            {
                float distance = Vector2.Distance(entity.transform.position, entity.buildings.allBuildings[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = entity.buildings.allBuildings[i];
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



    public float GetTargetDistance(Entity entity, Vector2 targetPos) =>
        Vector2.Distance(entity.characterRB.position, targetPos);
    public void MoveTo(Entity entity, Vector2 targetPos) =>
        entity.transform.position = Vector2.MoveTowards(
            entity.characterRB.position, targetPos,
            entity.movementSpeed * Time.deltaTime);


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


    public void Gather(Entity entity, Harvestable harvestable)
    {
        if (entity.currHitTime < entity.gatherSpeed)
            entity.currHitTime += Time.deltaTime;

        if (entity.currHitTime >= entity.gatherSpeed)
        {
            entity.currCarryingQuantity += harvestable.Gather(entity.gatherPerHit);
            entity.currHitTime = 0f;
        }
    }


    public void UpdateBrain(Entity entity)
    {
        if (entity.currBrain == EntityBrains.none)
        {
            entity.characterAnimator.SetBool("Idle", false);
            entity.characterAnimator.SetBool("Gather", false);
            entity.characterAnimator.SetBool("Build", false);
            entity.characterAnimator.SetBool("Health", false);
            entity.characterAnimator.SetBool("Battle", false);
        }

        if (entity.currBrain == EntityBrains.idle)
        {
            entity.characterAnimator.SetBool("Idle", true);
            entity.characterAnimator.SetBool("Gather", false);
            entity.characterAnimator.SetBool("Build", false);
            entity.characterAnimator.SetBool("Health", false);
            entity.characterAnimator.SetBool("Battle", false);
        }

        if (entity.currBrain == EntityBrains.gather)
        {
            entity.characterAnimator.SetBool("Idle", false);
            entity.characterAnimator.SetBool("Gather", true);
            entity.characterAnimator.SetBool("Build", false);
            entity.characterAnimator.SetBool("Health", false);
            entity.characterAnimator.SetBool("Battle", false);
        }

        if (entity.currBrain == EntityBrains.build)
        {
            entity.characterAnimator.SetBool("Idle", false);
            entity.characterAnimator.SetBool("Gather", false);
            entity.characterAnimator.SetBool("Build", true);
            entity.characterAnimator.SetBool("Health", false);
            entity.characterAnimator.SetBool("Battle", false);
        }

        if (entity.currBrain == EntityBrains.health)
        {
            entity.characterAnimator.SetBool("Idle", false);
            entity.characterAnimator.SetBool("Gather", false);
            entity.characterAnimator.SetBool("Build", false);
            entity.characterAnimator.SetBool("Health", true);
            entity.characterAnimator.SetBool("Battle", false);
        }

        if (entity.currBrain == EntityBrains.battle)
        {
            entity.characterAnimator.SetBool("Idle", false);
            entity.characterAnimator.SetBool("Gather", false);
            entity.characterAnimator.SetBool("Build", false);
            entity.characterAnimator.SetBool("Health", false);
            entity.characterAnimator.SetBool("Battle", true);
        }
    }


    //public enum ItemTypes
    //{
    //    COIN,
    //    SHARD,
    //    CRYSTAL,
    //    IDOL,
    //    RING,
    //    Max    //<-- not a real value; marks the end of the enum.
    //}

    //private void PickItem()
    //{
    //    ItemTypes randomItem = (ItemTypes)UnityEngine.Random.Range(0, (int)ItemTypes.Max);  // Picks an item at random
        
    //  // Or, to iterate through all types:
    //  for (int i = 0; i < (int)ItemTypes.Max; i++) {  }
    //}
}
