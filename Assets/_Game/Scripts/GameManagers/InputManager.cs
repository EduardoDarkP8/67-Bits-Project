using BitsProject;
using UnityEngine;
namespace BitsProject
{
    public class InputManager : Singleton<InputManager>
    {
        public Vector2 direction { get; private set; }
        public void AddJoyStick(JoyStick joyStick)
        {
            joyStick.OnDirectionChange += ChangeDirection;

        }
        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
        }
    }
}