using UnityEngine;

/*
 SCRIPT THAT CONTROLS THE PENGUIN AIS SOUND EFFECTS AND WHEN THEY PLAY
*/

public class SoundController : MonoBehaviour
{
    // REFERENCES TO AI'S ANIMATIONCONTROLLER SCRIPT AND SOUND EFFECTS
    public AnimationController animationController;
    public AudioSource footsteps;
    public AudioSource sliding;
    public AudioSource snoring;
    public AudioSource cuddling;
    public AudioSource eating;

    private void Update()
    {
        // FOOTSTEPS
        if (animationController.isMoving && !animationController.isSliding) // if is moving but not sliding
        {
            if (!footsteps.isPlaying) // if not already playing, start playing
            {
                footsteps.Play();
            }
        }
        else if (!animationController.isMoving)
        {
            footsteps.Stop();
        }
        // SLIDING
        if (animationController.isSliding)
        {
            if (!sliding.isPlaying)
            {
                sliding.Play();
            }
        }
        else if (!animationController.isSliding)
        {
            sliding.Stop();
        }
        // SLEEPING
        if (animationController.isSleeping)
        {
            if (!snoring.isPlaying) 
            {
                snoring.Play();
            }
        }
        else if (!animationController.isSleeping)
        {
            snoring.Stop();
        }
        // CUDDLING
        if (animationController.isCuddling)
        {
            if (!cuddling.isPlaying)
            {
                cuddling.Play();
            }
        }
        else if (!animationController.isCuddling)
        {
            cuddling.Stop();
        }
        // EATING
        if (animationController.isEating)
        {
            if (!eating.isPlaying)
            {
                eating.Play();
            }
        }
        else if (!animationController.isEating)
        {
            eating.Stop();
        }
    }
}

