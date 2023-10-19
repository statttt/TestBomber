using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.GamePlay;
using Game.GamePlay.Enemy;
using VContainer;
using Game.Scripts.Core;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;

namespace Game.Scripts.GamePlay.Setup
{
    [CreateAssetMenu]
    public class EnemyFactory : GameObjectFactory
    {
        [SerializeField]
        private Enemy _enemyPrefab;
        [SerializeField]
        private float _respawnEnemyDelay;

        private Player _player;
        private GameControl _gameControl;

        public void Initialize(Player player, GameControl gameControl)
        {
            _player = player;
            _gameControl = gameControl;
        }

        public Enemy Get()
        {
            Enemy enemy = CreateGameObjectInstance(_enemyPrefab);
            enemy.Initialize(_player, this);
            return enemy;
        }

        public async void Reclaim(Enemy enemy)
        {
            Vector3 lastPos = enemy.transform.position;
            Destroy(enemy.gameObject);
            await UniTask.Delay(TimeSpan.FromSeconds(_respawnEnemyDelay));
            _gameControl.SpawnEnemy(lastPos);
        }
    }
}


