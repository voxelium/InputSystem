using UnityEngine;
using UnityEngine.UI;

public class ButtonAttack : PlayerButton
{
    public override void Action()
    {
        playerController.Attack();
    }

}
