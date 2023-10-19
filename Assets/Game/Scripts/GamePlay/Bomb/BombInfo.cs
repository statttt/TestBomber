using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GamePlay.Setup
{
    [CreateAssetMenu]
    public class BombInfo : ScriptableObject
    {
        public BombType Type;
        public float Damage;
        public Color Color;
        public float ExplosionRadius;
    }

    [System.Serializable]
    public enum BombType
    {
        Small, 
        Medium,
        Large
    }
}
