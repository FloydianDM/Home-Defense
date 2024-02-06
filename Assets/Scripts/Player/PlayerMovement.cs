using UnityEngine;

namespace HomeDefense
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Defense _defense;
        
        private GridManager _gridManager;

        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();

            _gridManager.OnTouched += InstantiateDefense;
        }

        public void InstantiateDefense()
        {
            Vector3Int tilePosition = _gridManager.GetClickedTilePosition();
            
            bool isPlaceable = _gridManager.PlaceableCoordinatesDict[tilePosition];
            Vector3 clickedWorldPosition = _gridManager.Map.CellToWorld(tilePosition);

            Debug.Log(isPlaceable);

            if (isPlaceable)
            {
                bool isPlaced = _defense.CanCreateDefense(_defense, clickedWorldPosition);

                if (isPlaced)
                {
                    _gridManager.PlaceableCoordinatesDict[tilePosition] = false;
                }
            }
        }
    }
}

