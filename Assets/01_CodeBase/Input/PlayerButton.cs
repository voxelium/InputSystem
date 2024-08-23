
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerButton : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;
    private Button button;


    public void Constract(PlayerController player)
    {
        playerController = player;
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    public virtual void Action()
    { }

}
