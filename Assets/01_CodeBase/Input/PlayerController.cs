
using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private JoystickMovement _joystickMovement;
    private ButtonJump _buttonJump;
    private ButtonAttack _buttonFire;
    [SerializeField] private ShootFx _shootFx;

    [Header("Movement")]
    private float _moveSpeed = 4f;
    private float _rotateSpeed = 120f;
    private Vector3 charDirection;

    [Header("Gravity")]
    private float _gravityForce;

    [Header("Jump")]
    [SerializeField] private float _jumpTime = 0.8f;
    [SerializeField] private float _jumpHeight = 3f;
    private float _jumpVelocity;

    [Header("Character Component")]
    private CharacterController _characterController;
    public Vector2 moveValue;


    void Start()
    {
        _joystickMovement = FindObjectOfType<JoystickMovement>();
        _buttonJump = FindObjectOfType<ButtonJump>();
        _buttonJump.Constract(this);

        _buttonFire = FindObjectOfType<ButtonAttack>();
        _buttonFire.Constract(this);

        _characterController = GetComponent<CharacterController>();

        float takeoffTime = _jumpTime / 2;
        _gravityForce = 2 * _jumpHeight / Mathf.Pow(takeoffTime, 2);
        _jumpVelocity = 2 * _jumpHeight / takeoffTime;
    }


    void FixedUpdate()
    {
        Vector2 joystickInput = _joystickMovement.GetInputVector();

        // Если джойстик активен, используем его данные, иначе используем данные из Input Action
        Vector2 finalMoveValue = joystickInput.magnitude > 0 ? joystickInput : moveValue;

        ControlCharacter(finalMoveValue);
        Gravity();
    }


    private void ControlCharacter(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        MoveCharacter(moveDirection);
        RotateCharacter(moveDirection);
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        charDirection.x = moveDirection.x * _moveSpeed;
        charDirection.z = moveDirection.z * _moveSpeed;

        _characterController.Move(charDirection * Time.deltaTime);
    }


    public void RotateCharacter(Vector3 moveDirection)
    {
        if (_characterController.isGrounded)
        {
            float angle = Vector3.Angle(transform.forward, moveDirection);

            if (angle > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }


    //Название этого метода должно быть таким же как событие в InputActions, 
    //но с добавлением On
    private void OnMove(InputValue inputValue)
    {
        moveValue = inputValue.Get<Vector2>();
    }


    //Название этого метода должно быть таким же как событие в InputActions, 
    //но с добавлением On
    private void OnJump(InputValue inputValue)
    {
        Jump();
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            charDirection.y = _jumpVelocity;
        }
    }


    //Название этого метода должно быть таким же как событие в InputActions, 
    //но с добавлением On
    private void OnAttack(InputValue inputValue)
    {
        Attack();
    }

    public void Attack()
    {
        _shootFx.Shoot();
    }

    private void Gravity()
    {
        charDirection.y -= _gravityForce * Time.deltaTime;
    }

}
