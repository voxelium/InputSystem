using UnityEngine;

namespace GameInput
{
    public class JoystickMovement : JoystickHandler
    {
        public Vector2 GetInputVector()
        {
            return _inputVector;
        }
    }
}
