using Game.Scripts.GamePlay.Setup;
using Game.Scripts.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer _mesh;
    [SerializeField]
    private float _distanceForCollect;

    [SerializeField]
    private BombType _type;
    [SerializeField]
    private float _damage;

    private Player _player;
    private BombFactory _originFactory;

    public void Initialize(Player player, BombFactory bombFactory, BombInfo info)
    {
        _player = player;
        _originFactory = bombFactory;
        if(info != null)
        {
            _type = info.Type;
            _damage = info.Damage;
            _mesh.material.color = info.Color;
        }
    }

    private void Update()
    {
        CalculateDistanceToPlayer();
    }

    public void CalculateDistanceToPlayer()
    {
        if ((transform.position - _player.transform.position).sqrMagnitude <= _distanceForCollect)
        {
            _player.AddBomb(_type);
            _originFactory.Reclaim(this);
        }
    }

}
