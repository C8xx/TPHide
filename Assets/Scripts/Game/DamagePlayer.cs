using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damagable damagable = other.GetComponent<Damagable>();
            if (damagable != null)
            { 
                damagable.TakeDamage(damage);
                //Destroy(gameObject);
            }
        }
    }
}
