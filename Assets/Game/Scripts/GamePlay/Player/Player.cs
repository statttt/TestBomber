using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Game.Scripts.Core;
using Game.Scripts.GamePlay.Setup;

namespace Game.Scripts.GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _playerInfo;
        [SerializeField]
        private PlayerMovement _playerMovement;
        [SerializeField] private PlayerShoot _playerShoot;

        private GameControl _gameControl;
        public PlayerInfo PlayerInfo => _playerInfo;
        public PlayerShoot PlayerShoot => _playerShoot;

        public PlayerMovement PlayerMovement => _playerMovement;

        public void Initialize(GameControl gameControl, IInput input)
        {
            _gameControl = gameControl;
            _playerMovement.Initialize(input);
            _playerShoot.Initialize(gameControl);
        }

        public void AddBomb(BombType type)
        {
            _gameControl.AddBomb(type);
        }
    }
}

