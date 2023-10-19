using Game.Scripts.GamePlay.Setup;
using Game.Scripts.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using System;

namespace Game.GamePlay.Enemy
{
    [SelectionBase]
    public class Enemy : MonoBehaviour
    {
        private Player _player;
        private EnemyFactory _originFactory;
        private EnemyHealth _enemyHealth;

        public bool IsDie { get; private set; }

        public void Initialize(Player player, EnemyFactory enemyFactory)
        {
            _player = player;
            _originFactory = enemyFactory;
            IsDie = false;
        }

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.Initialize(this);
        }

        public void Getdamage(float damage)
        {
            _enemyHealth.GetDamage(damage);
        }

        internal void Die()
        {
            IsDie = true;
            _originFactory.Reclaim(this);
        }
    }
}
