using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip clip;
    public AudioClip clip2;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("lava!");
            collision.transform.position = CheckPointSystem.instance.UltimaPos;
            sound.PlayOneShot(clip);
        }
        if (collision.CompareTag("TP"))
        {
            print("lava!");
            Destroy(collision.gameObject);
            sound.PlayOneShot(clip2);
        }
    }
}
