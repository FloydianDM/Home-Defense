using System;
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
    
        public Tilemap Map => _map;
        private List<Vector3Int> _endPointCoordinates = new List<Vector3Int>();
        private List<Vector3Int> _spawnableCoordinates = new List<Vector3Int>();
        public Dictionary<TileBase, TileData> TileDict { get; private set; }
        public Dictionary<Vector3Int, bool> PlaceableCoordinatesDict;
        public event Action OnTouched;

        private void Awake()
        {
            InitializeTileDict();
            SetPlaceableTiles();
            SetFinishTiles();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                if (OnTouched != null)
                {
                    OnTouched();
                }

                GetClickedTilePosition();           
            }
            else
            {
                return;
            } 
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

        public Vector3 GetRandomSpawnPoint()
        {
            for (int x = _map.cellBounds.xMin; x < _map.cellBounds.xMax; x++)
            {
                for (int y = _map.cellBounds.yMin; y < _map.cellBounds.yMax; y++)
                {
                    TileBase tile = _map.GetTile(new Vector3Int(x, y));
                    
                    if (TileDict[tile].IsSpawnable)
                    {
                        Vector3Int tilePosition = new Vector3Int(x, y);

                        if (!_spawnableCoordinates.Contains(tilePosition))
                        {
                            _spawnableCoordinates.Add(tilePosition);   
                        }
                    }
                }
            }

            int randomTileIndex = UnityEngine.Random.Range(0, _spawnableCoordinates.Count);
            Vector3Int randomCoordinate = _spawnableCoordinates[randomTileIndex];

            Vector3 worldRandomCoordinate = _map.GetCellCenterWorld(randomCoordinate);

            return worldRandomCoordinate;
        }

        private void SetPlaceableTiles()
        {
            PlaceableCoordinatesDict = new Dictionary<Vector3Int, bool>();

            for (int x = _map.cellBounds.xMin; x < _map.cellBounds.xMax; x++)
            {
                for (int y = _map.cellBounds.yMin; y < _map.cellBounds.yMax; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y);
                    TileBase tile = _map.GetTile(tilePosition);
                    
                    if (!TileDict[tile].IsWalkable)
                    {
                        PlaceableCoordinatesDict[tilePosition] = true; 
                    }
                    else
                    {
                        PlaceableCoordinatesDict[tilePosition] = false; 
                    }
                }
            }
        }

        private void SetFinishTiles()
        {
            FlagCreator flagCreator = GetComponent<FlagCreator>();

            for (int x = _map.cellBounds.xMin; x < _map.cellBounds.xMax; x++)
            {
                for (int y = _map.cellBounds.yMin; y < _map.cellBounds.yMax; y++)
                {
                    TileBase tile = _map.GetTile(new Vector3Int(x, y));

                    if (TileDict[tile].IsEndPoint)
                    {
                        Vector3Int tilePosition = new Vector3Int(x, y);
                    
                        if (!_endPointCoordinates.Contains(tilePosition))
                        {
                            _endPointCoordinates.Add(tilePosition);
                        }
                    }
                }
            }

            foreach (Vector3Int tilePosition in _endPointCoordinates)
            {
                Vector3 worldTilePosition = _map.GetCellCenterWorld(tilePosition);
                flagCreator.CreateFlag(FlagType.EndFlag, worldTilePosition);
            }
        }
    }
}
