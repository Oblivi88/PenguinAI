using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public Image icon;

    public Sprite sleeping;
    public Sprite cuddling;

    public AnimationController penguinAnimationController;
    private void Update()
    {
        if (penguinAnimationController.isSleeping)
        {
            icon.sprite = sleeping;
            icon.enabled = true;
        }
        else if (penguinAnimationController.isCuddling)
        {
            icon.sprite = cuddling;
            icon.enabled = true;    
        }
        else
        {
            icon.enabled = false;
        }
    }
}
