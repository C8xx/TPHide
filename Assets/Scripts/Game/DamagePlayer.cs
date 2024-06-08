using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public AudioSource audioSource;
    public AudioClip destructionSound;

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
        else if (other.CompareTag("TP"))
        {
            Damagable damagable = other.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
                //Destroy(gameObject);
            }
            PlayDestructionSound();
           // Destroy(other.gameObject);
        }
    }

    private void PlayDestructionSound()
    {
        if (audioSource != null && destructionSound != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }
    }
}
