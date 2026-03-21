using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float speed;
    private NavMeshAgent navAgent;
    public AnimationController penguinAIcontroller;
    public NavMeshAgent penguinAiNavAgent;
    private Animator animator;

    private readonly int playerIsMovingHash = Animator.StringToHash("playerIsMoving");
    private readonly int playerIsCuddlingHash = Animator.StringToHash("playerIsCuddling");
    private bool playerIsMoving;
    private bool playerIsCuddling;

    public LayerMask terrainLayer; // ensures raycast only clicks on ground, not everything

    void Start()
    {
        speed = 50f;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            navAgent.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            navAgent.velocity = Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            navAgent.velocity = Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            navAgent.velocity = Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, float.MaxValue, terrainLayer))
            {
                navAgent.SetDestination(hitInfo.point);
            }
        }

        if (navAgent.velocity.magnitude >= 0.1f)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
        if (penguinAIcontroller.isCuddling && penguinAiNavAgent.remainingDistance <= 1.5)
        {
            navAgent.SetDestination(transform.position);
            playerIsCuddling = true;
        }
        else
        {
            playerIsCuddling = false;
        }

        animator.SetBool(playerIsMovingHash, playerIsMoving);
        animator.SetBool(playerIsCuddlingHash, playerIsCuddling);
    }
}
