using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public float sphereRadius = 0.2f;
    public float timeToDestroy = 5f;
    Rigidbody2D rb;
    public PlayerCamouflage player;
    public AudioSource sound;
    public AudioClip clip;
    public AudioClip clip2;
    public UnityEvent destroy;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(WaitToDestroy());
    }
    public void Push(Vector2 force)
    {
        rb.velocity = force;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, sphereRadius);
        if (hits.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {

                Teleport(transform.position);
                player.ApplyVelocity(rb.velocity);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                destroy.Invoke();
                sound.PlayOneShot(clip2);
                Destroy(gameObject);
            }
            
            Destroy(gameObject, 3f);
        }
    }
    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
    }


    public void Teleport(Vector2 position)
    {
        player.transform.position = position;
        sound.PlayOneShot(clip);
    }
}
