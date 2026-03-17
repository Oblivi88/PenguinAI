using NodeCanvas.Tasks.Actions;
using UnityEngine;

public class FishAnimation : MonoBehaviour
{
    private Animator animator;
    public AnimationController penguinAnimationController;

    // used in the Animator
    private readonly int isBeingEatenHash = Animator.StringToHash("isBeingEaten");

    // check if conditions are happening
    public bool isBeingEaten;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (penguinAnimationController.isEating)
        {
            isBeingEaten = true;
        }
        else
        {
            isBeingEaten = false;
        }
        animator.SetBool(isBeingEatenHash, isBeingEaten);

    }
}