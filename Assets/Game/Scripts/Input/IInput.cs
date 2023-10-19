using System;

namespace Game.Scripts
{
    public interface IInput
    {
        event Action<MovementDirection> InputEvent;
    }
}