using UnityEngine;

namespace HomeDefense
{
    public class Defense : MonoBehaviour
    {
        [SerializeField] private int defenseCost = 10;

        public bool CanCreateDefense(Defense defense, Vector3 createPosition)
        {
            CurrencySystem currencySystem = FindObjectOfType<CurrencySystem>();

            if (currencySystem == null)
            {
                return false;
            }

            if (currencySystem.CurrentBalance >= defenseCost)
            {
                currencySystem.WithdrawMoney(defenseCost);
                
                // TODO: Instantiate tower

                return true;
            }

            return false;
        }
    }   
}

