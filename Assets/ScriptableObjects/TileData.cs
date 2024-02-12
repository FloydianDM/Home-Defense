using UnityEngine;
using UnityEngine.Tilemaps;

namespace HomeDefense
{
    [CreateAssetMenu]
    public class TileData : ScriptableObject
    {
        public TileBase[] Tiles;
        public bool IsWalkable;
        public float WalkingSpeed;
        public bool IsSpawnable;
        public bool IsEndPoint;
    } 
}

