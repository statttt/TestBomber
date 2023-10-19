using System;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.GamePlay
{
    public class KeyboardInput : IInput, ITickable
    {
        public event Action<MovementDirection> InputEvent;


        void ITickable.Tick()
        {
            if (Input.GetKey(KeyCode.W))
                InputEvent?.Invoke(MovementDirection.Forward);

            if (Input.GetKey(KeyCode.S))
                InputEvent?.Invoke(MovementDirection.Back);

            if (Input.GetKey(KeyCode.D))
                InputEvent?.Invoke(MovementDirection.Right);

            if (Input.GetKey(KeyCode.A))
                InputEvent?.Invoke(MovementDirection.Left);
        }
    }
}
