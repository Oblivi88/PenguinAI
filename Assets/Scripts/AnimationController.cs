using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    // used in the Animator
    private readonly int isMovingHash = Animator.StringToHash("isMoving");

    // check if moving
    public bool isMoving;
    void Update()
    {
        animator.SetBool(isMovingHash, isMoving);
        if (agent.velocity.magnitude >= 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
