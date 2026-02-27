using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PenguinMove : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public LayerMask terrainLayers; // ensures raycast only clicks on ground, not everything
    private InputSystem_Actions inputActions;

    // footsteps sound effect handling
    private bool penguinIsMoving = false;
    public AudioSource walking;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        
    }

    private void Update()
    {
        // if the velocity is not 0, it is moving. This will tell the sound effect when to play.
        if (navAgent.velocity.x != 0 || navAgent.velocity.z != 0)
        {
            penguinIsMoving = true;
        } 
        else
        {
            penguinIsMoving = false;
        }

        if (penguinIsMoving)
        {
            if (walking.isPlaying == false)
            {
                walking.Play();
            }
        }
        else
        {
            if (!walking.isPlaying)
            {
                walking.Stop();
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, float.MaxValue, terrainLayers))
        {
            navAgent.SetDestination(hitInfo.point);
        }
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Disable();
    }
}