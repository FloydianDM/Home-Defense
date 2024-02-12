using UnityEngine;

namespace HomeDefense
{
    public enum FlagType
    {
        EndFlag
    }

    public class FlagCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _endFlag;

        public void CreateFlag(FlagType flagType, Vector3 spawnPoint)
        {
            switch (flagType)
            {
                case FlagType.EndFlag:
                    Instantiate(_endFlag, spawnPoint, Quaternion.identity);
                    break;
            }
        }
    }
}

