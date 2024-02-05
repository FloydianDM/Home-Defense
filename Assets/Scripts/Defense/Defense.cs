using UnityEngine;

namespace HomeDefense
{
    public class Defense : MonoBehaviour
    {
        [SerializeField] private int _defenseCost = 10;

        public bool CanCreateDefense(Defense defense, Vector3 createPosition)
        {
            CurrencySystem currencySystem = FindObjectOfType<CurrencySystem>();

            if (currencySystem == null)
            {
                return false;
            }

            if (currencySystem.CurrentBalance >= _defenseCost)
            {
                currencySystem.WithdrawMoney(_defenseCost);
                Instantiate(defense, createPosition, Quaternion.identity);

                return true;
            }

            return false;
        }
    }   
}

