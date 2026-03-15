using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    // used in the Animator
    private readonly int isMovingHash = Animator.StringToHash("isMoving");
    private readonly int isSleepingHash = Animator.StringToHash("isSleeping");
    private readonly int isEatingHash = Animator.StringToHash("isEating");

    // check if conditions are happening
    private bool isMoving;
    public bool isSleeping;
    public bool isEating;
    void Update()
    {
        if (agent.velocity.magnitude >= 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        animator.SetBool(isMovingHash, isMoving);
        animator.SetBool(isSleepingHash, isSleeping);
        animator.SetBool(isEatingHash, isEating);

    }
}
