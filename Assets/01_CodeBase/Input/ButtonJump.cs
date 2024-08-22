using UnityEngine;
using UnityEngine.UI;

public class ButtonJump : MonoBehaviour
{
    [HideInInspector] public CharacterMovement characterMovement;
    private Button button;

    public void Constract(CharacterMovement character)
    {
        characterMovement = character;
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    private void Action()
    {
        characterMovement.Jump();
    }


}
