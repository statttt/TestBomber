using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.GamePlay
{
    public class PlayerSpawner  : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private Transform _startPoint;

        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            _player.transform.position = _startPoint.position;
        }

        public void Initialize() { }
    }
}
