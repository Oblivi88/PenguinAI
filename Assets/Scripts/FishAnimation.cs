using UnityEngine;

/*
SCRIPT THAT CONTROLS THE ANIMATION OF THE FISH THAT THE PENGUIN AI EATS
*/
public class FishAnimation : MonoBehaviour
{
    // references to the fish's animator and the penguin AI's animationController script
    private Animator animator;
    public AnimationController penguinAnimationController;

    // used in the Animator
    private readonly int isBeingEatenHash = Animator.StringToHash("isBeingEaten");

    // check if penguin is eating
    public bool isBeingEaten;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // if penguin is eating, play animation 
        if (penguinAnimationController.isEating)
        {
            isBeingEaten = true;
        }
        else
        {
            isBeingEaten = false;
        }
        // to animator
        animator.SetBool(isBeingEatenHash, isBeingEaten);

    }
}