using UnityEngine;
using UnityEngine.UI;

public class ButtonJump : PlayerButton
{
    public override void Action()
    {
        playerController.Jump();
    }

}
