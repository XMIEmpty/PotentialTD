﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFarming : MonoBehaviour, IState
{
    private Entity entity;
    private FertileLand currFertileLand;
    private Hall currHall;

    private Harvestable currHarvestable;

    public IFarming(Entity entity) => this.entity = entity;


    public bool Condition() =>
        entity.currState == EntityStates.Farming;


    public void Execute()
    {
        /*
         * ADAPT THIS SHIT USING Harvestable.cs
         * Use:
         *      harvestableInteractions = none/ gather/ place
         *      harvestableMaterial     = none/ souls/ mushLogs/ food
         *      plantType               = none/ dirt/ plant/ shrub/ tree/ stone
         *      string plantTag;
         */

        // Find Closest Fertile Land
        // Go To It
        // Harvest It

        if (entity.goPlant == false && entity.returnHarvested == false)
        {
            if (entity.target == null)
            {// Get Available Target + Set Target Layer To Unavailable + Set Fertile Land +
             // Set Current Fertile Land Harvester To this.Entity
                entity.target = entity.stateMethods.GetClosestThingAvailable(entity, "FertileLand");

                //landInstanceID = entity.target.GetInstanceID();
                currFertileLand = entity.target.GetComponent<FertileLand>();
                currFertileLand.harvester = entity.gameObject;
            }
            if (entity.stateMethods.GetTargetDistance(entity) > 1.0f)
            {// Look Towards Target + Move Towards Looking Direction
                entity.stateMethods.LookAtTarget(entity);
                entity.stateMethods.Chase(entity);
            }
            if (entity.stateMethods.GetTargetDistance(entity) <= 1.0f)
            {// Velocity 0 + Harvesting + Harvest Complete, Find Hall
                if (entity.characterRB.velocity != new Vector2(0.0f, 0.0f)) // Velocity = 0
                    entity.characterRB.velocity = new Vector2(0.0f, 0.0f);

                if (entity.currWorkTime < currFertileLand.timeToHarvest &&
                    entity.currWorkTime < entity.maxWorkTime) // Count Up Work Time
                    entity.currWorkTime += Time.deltaTime;

                if (entity.currWorkTime >= currFertileLand.timeToHarvest ||
                    entity.currWorkTime >= entity.maxWorkTime)
                {// Harvest Complete + Get Closest Hall + Return_Harvested Set True
                    currFertileLand.Harvest(entity.currWorkTime);
                    if (entity.stateMethods.GetClosestThing(entity, "Hall"))
                    {

                    }
                    entity.target = entity.stateMethods.GetClosestThing(entity, "Hall");
                    Debug.Log("NNN New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
                    entity.returnHarvested = true;
                }
            }
        }















        // Find Closest Fertile Land
            // Go To It
            // Harvest It

        if (entity.goPlant == false && entity.returnHarvested == false /*&& entity.currWorkTime < 0.1f*/)
        {
            if (entity.target == null)
            {// Get Available Target + Set Target Layer To Unavailable + Set Fertile Land +
             // Set Current Fertile Land Harvester To this.Entity
                entity.target = entity.stateMethods.GetClosestThingAvailable(entity, "FertileLand");

                //landInstanceID = entity.target.GetInstanceID();
                currFertileLand = entity.target.GetComponent<FertileLand>();
                currFertileLand.harvester = entity.gameObject;
            }

            if (entity.stateMethods.GetTargetDistance(entity) > 1.0f)
            {// Look Towards Target + Move Towards Looking Direction
                entity.stateMethods.LookAtTarget(entity);
                entity.stateMethods.Chase(entity);
            }

            if (entity.stateMethods.GetTargetDistance(entity) <= 1.0f)
            {// Velocity 0 + Harvesting + Harvest Complete, Find Hall
                if (entity.characterRB.velocity != new Vector2(0.0f, 0.0f)) // Velocity = 0
                    entity.characterRB.velocity = new Vector2(0.0f, 0.0f);

                if (entity.currWorkTime < currFertileLand.timeToHarvest &&
                    entity.currWorkTime < entity.maxWorkTime) // Count Up Work Time
                    entity.currWorkTime += Time.deltaTime;

                if (entity.currWorkTime >= currFertileLand.timeToHarvest ||
                    entity.currWorkTime >= entity.maxWorkTime)
                {// Harvest Complete + Get Closest Hall + Return_Harvested Set True
                    currFertileLand.Harvest(entity.currWorkTime);
                    if (entity.stateMethods.GetClosestThing(entity, "Hall"))
                    {
                        
                    }
                    entity.target = entity.stateMethods.GetClosestThing(entity, "Hall");
                    Debug.Log("NNN New Target acquired: " + entity.target.name + "\n\r location: " + entity.target.transform.position);
                    entity.returnHarvested = true;
                }
            }
        }

        // Find Closest Hall or Storage
        // Go To It
        // Deliver Harvested Item

        if (entity.goPlant == false && entity.returnHarvested == true)
        {
            if (entity.target.tag == "Hall" && currHall == null)
            {// Set Hall
                currHall = entity.target.GetComponent<Hall>();
            }

            if (entity.stateMethods.GetTargetDistance(entity) > 1f)
            {// Look Towards Target + Move Towards Looking Direction
                entity.stateMethods.LookAtTarget(entity);
                entity.stateMethods.Chase(entity);
            }

            if (entity.stateMethods.GetTargetDistance(entity) < 1f)
            {// Velocity Set 0 + Deliver Food + Set Current Work To 0 +
             // Return_Harvested Set False + Go_Plant Set True + currHall Set Null
                if (entity.characterRB.velocity != new Vector2(0.0f, 0.0f)) // Velocity = 0
                    entity.characterRB.velocity = new Vector2(0.0f, 0.0f);

                currHall.GetFood(Mathf.RoundToInt(entity.currWorkTime));
                entity.currWorkTime = 0;
                entity.returnHarvested = false;
                entity.goPlant = true;
                currHall = null;
            }
        }

        // Go Back To (now) Empty Land
        // Replant It
        
        if (entity.goPlant == true && entity.returnHarvested == false)
        {            
            if (entity.stateMethods.GetLastTargetDistance(entity) > 1f)
            {// Look Towards Target + Move Towards Looking Direction
                entity.stateMethods.LookAtLastTarget(entity);
                entity.stateMethods.Chase(entity);
                Debug.Log("Moving To EmptyLand");
            }

            if (entity.stateMethods.GetLastTargetDistance(entity) < 1f)
            {
                if (entity.characterRB.velocity != new Vector2(0.0f, 0.0f)) // Velocity = 0
                    entity.characterRB.velocity = new Vector2(0.0f, 0.0f);

                if (entity.currWorkTime < currFertileLand.timeToPlant &&
                    entity.currWorkTime < entity.maxWorkTime)
                    entity.currWorkTime += Time.deltaTime;

                if (entity.currWorkTime >= currFertileLand.timeToPlant ||
                    entity.currWorkTime >= entity.maxWorkTime)
                {
                    Debug.Log("Plant!");
                    currFertileLand.Plant(entity.currWorkTime);
                    entity.goPlant = false;
                    ResetValues();
                }
            }
        }
    }

    // Bool - ReturnHarvested
    // Bool - GoPlant
    // GameObject Target
    // GameObject LastTarget

    private void ResetValues()
    {
        entity.target = null;
        entity.lastTarget = null;
        currFertileLand = null;
        currHall = null;
    }
}
