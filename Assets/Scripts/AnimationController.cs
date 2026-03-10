using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    // used in the Animator
    private readonly int isMovingHash = Animator.StringToHash("isMoving");

    // check for moving
    public bool isMoving;

    void Update()
    {
        if (agent.velocity.magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
