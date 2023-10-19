using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using VContainer;

namespace Game.Scripts.GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private float _speed;
        private Player _player;
        private IInput _input;
        private Rigidbody _rigidbody;

        public void Initialize(IInput input)
        {
            _input = input;

            _input.InputEvent += InputHandler;
        }


        private void Start()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();

            _speed = _player.PlayerInfo.Speed;
        }


        private void InputHandler(MovementDirection direction)
        {
            if (_player.PlayerShoot.IsAminig)
            {
                return;
            }
            Vector3 moment = GetDirectionVector(direction) * _speed * Time.deltaTime;
            transform.position += moment;
        }

        private static Vector3 GetDirectionVector(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Forward:
                    return new Vector3(0, 0, 1);
                case MovementDirection.Right:
                    return new Vector3(1, 0, 0);
                case MovementDirection.Back:
                    return new Vector3(0, 0, -1);
                case MovementDirection.Left:
                    return new Vector3(-1, 0, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void OnDestroy()
        {
            _input.InputEvent -= InputHandler;
        }
    }
}
