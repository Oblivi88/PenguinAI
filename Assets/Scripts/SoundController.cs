using UnityEngine;
using UnityEngine.AI;

public class SoundController : MonoBehaviour
{
    public AnimationController animationController;
    public AudioSource footsteps;
    public AudioSource sliding;

    private void Update()
    {
        if (animationController.isMoving && !animationController.isSliding)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (!animationController.isMoving)
        {
            footsteps.Stop();
        }
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
    }
}

