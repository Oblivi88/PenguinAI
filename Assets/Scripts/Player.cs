using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


/*
 SCRIPT THAT CONTROLS EVERYTHING RELATED TO THE PLAYER
 THIS INCLUDES MOVEMENT AND ANIMATION
*/
public class Player : MonoBehaviour
{
    // references to players navMeshAgent and animator, as well as penguin AI's animationController script and navMeshAgent
    private NavMeshAgent navAgent;
    public AnimationController penguinAIcontroller;
    public NavMeshAgent penguinAINavAgent;
    private Animator animator;
    // animations
    private readonly int playerIsMovingHash = Animator.StringToHash("playerIsMoving");
    private readonly int playerIsCuddlingHash = Animator.StringToHash("playerIsCuddling");
    private bool playerIsMoving;
    private bool playerIsCuddling;
    //footsteps
    private AudioSource footsteps;

    public LayerMask terrainLayer; // ensures raycast only clicks on ground, not everything

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        footsteps = GetComponent<AudioSource>();
    }
    void Update()
    {
        // CLICK TO MOVE
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, float.MaxValue, terrainLayer))
            {
                navAgent.SetDestination(hitInfo.point);
            }
        }

        // MOVEMENT CHECK (PLAYER)
        if (navAgent.velocity.magnitude >= 0.1f)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
        // CUDDLING CHECK (PLAYER)
        if (penguinAIcontroller.isCuddling && penguinAINavAgent.remainingDistance <= 1.5)
        {
            navAgent.SetDestination(transform.position);
            playerIsCuddling = true;
        }
        else
        {
            playerIsCuddling = false;
        }

        // footsteps sound
        if (playerIsMoving)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (!playerIsMoving)
        {
            footsteps.Stop();
        }
        // to animator (player)
        animator.SetBool(playerIsMovingHash, playerIsMoving);
        animator.SetBool(playerIsCuddlingHash, playerIsCuddling);
    }
}
