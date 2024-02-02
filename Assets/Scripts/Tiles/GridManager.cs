using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace HomeDefense
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap _map;
        [SerializeField] private List<TileData> _tileDataList;
    
        private List<Vector3Int> _spawnableCoordinates = new List<Vector3Int>();
        public Dictionary<TileBase, TileData> TileDict { get; private set; }
        public Tilemap Map => _map;

        private void Awake()
        {
            InitializeTileDict();
        }

        private void InitializeTileDict()
        {
            TileDict = new Dictionary<TileBase, TileData>();

            foreach (TileData tileData in _tileDataList)
            {
                foreach (TileBase tile in tileData.Tiles)
                {
                    TileDict.Add(tile, tileData);
                }
            }
        }

        public Vector3Int GetClickedTilePosition()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
            Vector3Int gridPosition = _map.WorldToCell(worldPosition);
            
            return gridPosition;
        }
        
        public Vector3Int GetTilePosition(Vector2 position)
        {
            Vector3Int gridPosition = _map.WorldToCell(position);

            return gridPosition;
        }

        public float GetTileSpeed(Vector2 position)
        {
            Vector3Int gridPosition = GetTilePosition(position);
            TileBase tile = _map.GetTile(gridPosition);

            return TileDict[tile].WalkingSpeed;
        }

        public bool IsTileSpawnable(Vector2 position)
        {
            Vector3Int gridPosition = GetTilePosition(position);
            TileBase tile = _map.GetTile(gridPosition);

            return TileDict[tile].IsSpawnable;
        }

        public TileBase GetClickedTileType()
        { 
            Vector3Int gridPosition = GetClickedTilePosition();
            TileBase clickedTile = _map.GetTile(gridPosition);

            return clickedTile;
        }

        public Vector3 GetRandomSpawnPoint()
        {
            for (int x = _map.cellBounds.xMin; x < _map.cellBounds.xMax; x++)
            {
                for (int y = _map.cellBounds.yMin; y < _map.cellBounds.yMax; y++)
                {
                    TileBase tile = _map.GetTile(new Vector3Int(x, y));
                    
                    if (TileDict[tile].IsSpawnable)
                    {
                        var tilePosition = new Vector3Int(x, y);

                        if (!_spawnableCoordinates.Contains(tilePosition))
                        {
                            _spawnableCoordinates.Add(tilePosition);   
                        }
                    }
                }
            }

            int randomTileIndex = Random.Range(0, _spawnableCoordinates.Count);
            Vector3Int randomCoordinate = _spawnableCoordinates[randomTileIndex];

            Vector3 worldRandomCoordinate = _map.CellToWorld(randomCoordinate);

            return worldRandomCoordinate;
        }
    }
}
