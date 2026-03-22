using UnityEngine;
using UnityEngine.AI;

/*
 SCRIPT TO CONTROL PENGUIN AIS ANIMATIONS
 USING ANIMATOR
*/
public class AnimationController : MonoBehaviour
{
    // references to AI's animator and navMeshAgent
    private Animator animator;
    private NavMeshAgent agent;

    // used in the Animator
    private readonly int isMovingHash = Animator.StringToHash("isMoving");
    private readonly int isSleepingHash = Animator.StringToHash("isSleeping");
    private readonly int isSlidingHash = Animator.StringToHash("isSliding");
    private readonly int isEatingHash = Animator.StringToHash("isEating");
    private readonly int isCuddlingHash = Animator.StringToHash("isCuddling");

    // check if conditions are happening
    public bool isMoving;
    public bool isSleeping;
    public bool isSliding;
    public bool isEating;
    public bool isCuddling;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        // MOVCEMENT CHECK
        if (agent.velocity.magnitude >= 0.1f) 
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // set to animator
        animator.SetBool(isMovingHash, isMoving);
        animator.SetBool(isSleepingHash, isSleeping);
        animator.SetBool(isSlidingHash, isSliding);
        animator.SetBool(isEatingHash, isEating);
        animator.SetBool(isCuddlingHash, isCuddling);

    }
}
