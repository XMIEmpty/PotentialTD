using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileLand : MonoBehaviour
{
    Animator characterAnimator;
    

    public float currTime, timeToStageII, timeToStageIII;
    public float timeToHarvest, timeToPlant;
    public bool isGrown, isEmpty;

    public GameObject harvester;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        //if (harvester == null) gameObject.layer = LayerMask.NameToLayer("Available");
        //else if (harvester != null) gameObject.layer = LayerMask.NameToLayer("Unavailable");
        if (isGrown == false)
        {
            currTime += Time.deltaTime;

            if (currTime > timeToStageII)
            {

                // Stage 2 Sprite
                characterAnimator.SetInteger("Stage", 2);
                if (currTime > timeToStageII + timeToStageIII)
                {
                    // Stage 3 Sprite
                    characterAnimator.SetInteger("Stage", 3);
                    gameObject.tag = "FertileLand";
                    isGrown = true;
                }
            }
            else if (currTime < timeToStageII)
            {
                // Stage 1 Sprite
                characterAnimator.SetInteger("Stage", 1);
            }
        }

    }


    public void Harvest(float workTime)
    {
        if (workTime > timeToHarvest)
        {
            // Stage 0 Sprite
            characterAnimator.SetInteger("Stage", 0);
            currTime = 0;
            gameObject.tag = "EmptyLand";
        }
    }


    public void Plant(float workTime)
    {
        if (workTime > timeToPlant)
        {
            // Stage 1 Sprite
            characterAnimator.SetInteger("Stage", 1);
            gameObject.tag = "UnfertileLand";
            isGrown = false;
            harvester = null;
        }
    }
}
