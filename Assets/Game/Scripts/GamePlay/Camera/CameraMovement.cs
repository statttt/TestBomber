using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GamePlay
{
    public class CameraMovement : MonoBehaviour
    {
        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            if(_player != null)
            {
                transform.position = _player.transform.position;
            }
        }
    }
}