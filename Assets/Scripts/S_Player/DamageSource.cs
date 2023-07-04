using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    // This variable defines the amount of damage that the weapon is going to make.
    [SerializeField] private int _damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* This class validade if, once the object marked as trigger collide with the component defined in the if condition, if so will do a action
         * #1 - This collision access the enemy's Health Script and  pass the value of the damage taken when entered in the collision defined.
         */

        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(_damageAmount);
        }
    }


    
}
