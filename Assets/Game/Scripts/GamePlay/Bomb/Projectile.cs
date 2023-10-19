using Game.Scripts.GamePlay.Setup;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;
using Game.GamePlay.Enemy;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _mesh;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private LayerMask _layerEnemy;

    private Rigidbody _rigidbody;
    private Vector3 _targetPosition;
    private Transform _startParent;
    private Transform _explosionPrefab;
    private Vector3 _startSize;
    private float _damage;
    private float _explosionRadius;

    private bool _isExplosion;
    private bool _isActive;

    public void Initialize(Transform parent)
    {
        _startParent = parent;
        _startSize = _model.transform.localScale;
        Deactivate(transform);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isActive && !_isExplosion)
        {
            if ((transform.position - _targetPosition).sqrMagnitude <= 0.1f)
            {
                Explosion();
            }
        }
    }

    public void SetVelocity(Vector3 velocity, Vector3 targetPosition)
    {
        _rigidbody.velocity = velocity;
        _targetPosition = targetPosition;
    }

    public void Activate(Transform newParent, BombInfo activeBomb)
    {
        _isActive = true;
        transform.parent = newParent;
        _rigidbody.isKinematic = false;
        _mesh.material.color = activeBomb.Color;
        _damage = activeBomb.Damage;
        _explosionRadius = activeBomb.ExplosionRadius;
        gameObject.SetActive(true);
    }

    public void Deactivate(Transform parent)
    {
        _isActive = false;
        _isExplosion = false;
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
        _model.transform.localScale = _startSize;
    }

    public async void Explosion()
    {
        _isExplosion = true;
        _rigidbody.isKinematic = true;
        float explosionSize = _explosionRadius * 2f;
        _model.transform.localScale = Vector3.one * explosionSize;
        FindEnemies(_explosionRadius);
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        Deactivate(transform);
    }

    private void FindEnemies(float radius)
    {
        Collider[] _targets = Physics.OverlapSphere(transform.localPosition, radius, _layerEnemy);
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].GetComponent<Enemy>().Getdamage(_damage);
        }
    }
}
