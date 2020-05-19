using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityBrains { none = 0, idle = 1, gather = 2, building = 3, health = 4, battle = 5}
public enum EntityStates { Idle = 0, Sleep = 1, Eat = 2, Building = 3, MushTreeChopping = 5, Farming = 6, SoulHarvesting = 7}

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Entity : MonoBehaviour
{
    public EntityBrains currBrain;
    public EntityStates currState;

    public GameObject target, lastTarget;
    public GameObject facingDirection;
    public StateMethods stateMethods;

    [Range(0.5f, 9f)]
    public float attackRadius;
    [SerializeField]
    public float sightRange;
    private bool showSightRange;
    public int movementSpeed;
    public float currWorkTime;
    public float maxWorkTime;
    public float waitTime;
    public Animator characterAnimator;
    public Animator charIconAnimator;
    public Rigidbody2D characterRB;
    public GameObject gameManager;
    public Buildings buildings;

    public bool returnHarvested = false, goPlant = false;


    public Attribute[] attributes;

    [System.Serializable]
    public class Attribute
    {
        public string statName;
        public float statValue;
    }


    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        //charIconAnimator = transform.GetChild(1).GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody2D>();
        //abilityToCast = gameManager.GetComponent<AbilityLists>().banditAbilities[0];

        if(buildings.farms[2].lands[3].harvester != null)
        { /*available*/}
    }


    

    private void FixedUpdate()
    {

        ApplyMovementInput();
    }


    private void ApplyMovementInput()
    {
            float moveHorizontal = characterRB.velocity.x;
            float moveVertical = characterRB.velocity.y;

            MovementAnimationUpdate(moveHorizontal, moveVertical);
    }


    // Dimitrios Kitsikidis
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
