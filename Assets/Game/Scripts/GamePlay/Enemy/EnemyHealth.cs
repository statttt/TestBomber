using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.GamePlay.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private int _health;
        [SerializeField]
        private int _healthOffset;
        [SerializeField]
        private TMP_Text _textHealth;

        private Enemy _enemy;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
        }

        private void Start()
        {
            _health = Random.Range(_health - _healthOffset, _health + _healthOffset + 1);
            UpdateText();
        }

        private void Update()
        {
            if (_enemy != null && !_enemy.IsDie)
            {
                _textHealth.transform.parent.LookAt(Camera.main.transform.position);
            }
        }

        public void GetDamage(float damage)
        {
            _health -= (int)damage;
            if(_health <= 0)
            {
                if(_enemy != null)
                {
                    _enemy.Die();
                }
            }
            else
            {
                UpdateText();
            }
        }

        public void UpdateText()
        {
            _textHealth.text = _health.ToString();
        }
    }
}