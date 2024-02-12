using UnityEngine;

namespace HomeDefense
{
    public class EnemyCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.CompareTag("EndPoint"))
            {
                EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
                enemyMovement.StealMoney();
            }    
        }
    }   
}

