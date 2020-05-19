using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityBrains { none = 0, idle = 1, gather = 2, build = 3, health = 4, battle = 5}
public enum EntityStates { Idle = 0, Sleep = 1, Eat = 2, Building = 3, MushTreeChopping = 5, Farming = 6, SoulHarvesting = 7}

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Entity : MonoBehaviour
{
    [Header("Status")]
    public EntityBrains currBrain;
    public EntityStates currState;

    [Header("Target")]
    [Space(25)]
    public StateMethods stateMethods;
    public GameObject facingDirection;
    public GameObject target, lastTarget;
    public Vector2 targetPos, lastTargetPos;

    [Header("Movement")]
    [SerializeField]
    [Space(25)]
    private bool showSightRange;
    [Range(0.0f, 100f)]
    public float sightRange;
    [Range(0, 10)]
    public int movementSpeed;

    [Header("Work Related")]
    [Space(25)]
    public int currCarryingQuantity;
    public int gatherPerHit;
    [Range(0.0f, 3f)]
    public float gatherSpeed, currHitTime;

    public float currWorkTime, maxWorkTime;
    public bool returnHarvested = false, goPlant = false;

    [Header("Generic")]
    [Space(25)]
    public GameObject gameManager;
    public Animator characterAnimator, charIconAnimator;
    public Rigidbody2D characterRB;
    public Buildings buildings;





    //public Attribute[] attributes;

    //[System.Serializable]
    //public class Attribute
    //{
    //    public string statName;
    //    public float statValue;
    //}


    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        buildings = gameManager.transform.GetChild(0).gameObject.GetComponent<Buildings>();
        //charIconAnimator = transform.GetChild(1).GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody2D>();
        //abilityToCast = gameManager.GetComponent<AbilityLists>().banditAbilities[0];

        //if(buildings.farms[2].lands[3].harvester != null)
        //{ /*available*/}
    }


    private void FixedUpdate()
    {
        //ApplyMovementInput();
    }


    private void ApplyMovementInput()
    {
            float moveHorizontal = characterRB.velocity.x;
            float moveVertical = characterRB.velocity.y;
            MovementAnimationUpdate(moveHorizontal, moveVertical);
    }


    /// <summary>
    /// Updates the [Player's Animation]
    /// based on    [Player's Movement].
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    private void MovementAnimationUpdate(float moveX, float moveY)
    {
        // Changes Animation Based on direction facing.
        characterAnimator.SetFloat("FaceX", moveX);
        characterAnimator.SetFloat("FaceY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            characterAnimator.SetBool("isWalking", true);
            if (moveX > 0) characterAnimator.SetFloat("LastMoveX", 1f);
            else if (moveX < 0) characterAnimator.SetFloat("LastMoveX", -1f);
            else characterAnimator.SetFloat("LastMoveX", 0f);

            if (moveY > 0) characterAnimator.SetFloat("LastMoveY", 1f);
            else if (moveY < 0) characterAnimator.SetFloat("LastMoveY", -1f);
            else characterAnimator.SetFloat("LastMoveY", 0f);
        }
        else
        {
            characterAnimator.SetBool("isWalking", false);
        }
    }


    /// <summary>
    /// Draws The Apropriate Gizmos
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (showSightRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, sightRange);
        }
    }
}
