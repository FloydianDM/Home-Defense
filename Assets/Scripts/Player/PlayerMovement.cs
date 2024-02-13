using UnityEngine;

namespace HomeDefense
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Defense _rangedDefense;
        [SerializeField] private Defense _meleeDefense;
        
        private GridManager _gridManager;
        private AudioManager _audioManager;
        public DefenseType DefenseType;

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _gridManager = FindObjectOfType<GridManager>();

            _gridManager.OnTouched += InstantiateDefense;
        }

        private void InstantiateDefense()
        {
            Vector3Int tilePosition = _gridManager.GetClickedTilePosition();

            if (!_gridManager.PlaceableCoordinatesDict.ContainsKey(tilePosition))
            {
                return;
            }
            
            bool isPlaceable = _gridManager.PlaceableCoordinatesDict[tilePosition];
            Vector3 clickedWorldPosition = _gridManager.Map.GetCellCenterWorld(tilePosition);

            if (isPlaceable)
            {
                bool isPlaced = CanSelectTowerType(clickedWorldPosition);

                if (isPlaced)
                {
                    _audioManager.PlayCreateTowerSFX();
                    _gridManager.PlaceableCoordinatesDict[tilePosition] = false;
                }
            }
        }

        private bool CanSelectTowerType(Vector3 clickedWorldPosition)
        {
            bool isPlaced = false;

            switch (DefenseType)
            {
                case DefenseType.ranged:
                    isPlaced = _rangedDefense.CanCreateDefense(_rangedDefense, clickedWorldPosition);
                    break;
                case DefenseType.melee:
                    isPlaced = _meleeDefense.CanCreateDefense(_meleeDefense, clickedWorldPosition);
                    break;
            }

            return isPlaced;
        }
    }
}

