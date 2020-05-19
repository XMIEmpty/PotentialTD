using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushTree : MonoBehaviour
{
    public Animator characterAnimator;


    public float currTime, timeToStageII, timeToStageIII;
    public float timeToHarvest, timeToPlant;
    public bool isGrown;

    public int currHealth, maxHealth;
    public float growthCountDown, timeToSappler, toMushTree;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        currHealth = maxHealth;
        growthCountDown = 0.00f;
    }


    private void Update()
    {
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (isGrown == false) currTime += Time.deltaTime;

        if (currTime > timeToStageII)
        {
            // Stage 2 Sprite
            characterAnimator.SetInteger("Stage", 1);
            if (currTime > timeToStageII + timeToStageIII)
            {
                // Stage 3 Sprite
                characterAnimator.SetInteger("Stage", 2);
                isGrown = true;
            }
        }
        else
        {
            // Stage 1 Sprite
            characterAnimator.SetInteger("Stage", 0);
        }
    }


    public void Harvest(int workAmount)
    {
        currHealth -= workAmount;
    }


    public bool Plant(float workTime)
    {
        if (workTime > timeToPlant)
        {
            // Stage 1 Sprite
            characterAnimator.SetInteger("Stage", 0);
            isGrown = false;
            currTime = 0;
            return true;
        }
        return false;
    }
}
