using UnityEngine;
using UnityEngine.UI;

/*
 SCRIPT THAT CONTROLS THE PENGUIN AIS ICONS
*/
public class IconController : MonoBehaviour
{
    // references to icon as a child of the penguin object, the two source sprites, and the animationController script
    public Image icon;

    public Sprite sleeping;
    public Sprite cuddling;

    public AnimationController penguinAnimationController;
    private void Update()
    {
        // IF PENGUIN IS SLEEPING OR CUDDLING, DISPLAY APPROPRIATE ICON
        // IF NOT, HIDE ICONS
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
