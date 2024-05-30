using UnityEngine;
using System.Collections; // Añadido para IEnumerator

public class PlayerCamouflage : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    public GameObject playerObject;
    public GameObject camouflagedObject;

    [Header("Configuración de Entrada")]
    public KeyCode camouflageKey = KeyCode.C;

    [Header("Duración del Camuflaje")]
    public float camouflageDuration = 5.0f;

    private bool isCamouflaged = false;
    private Coroutine camouflageCoroutine;
    public bool canThrow;
    public Transform SpawnPoint;
    public float throwForce;
    public GameObject bomb;
    private Rigidbody2D rb;
   private AudioSource sfx;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(camouflageKey))
        {
            ToggleCamouflage();
        }
    }

    private void ToggleCamouflage()
    {
        if (isCamouflaged)
        {
            StopCamouflage();
        }
        else
        {
            StartCamouflage();
        }
    }

    private void StartCamouflage()
    {
        isCamouflaged = true;
        playerObject.SetActive(false);
        camouflagedObject.SetActive(true);

        if (camouflageCoroutine != null)
        {
            StopCoroutine(camouflageCoroutine);
        }
        camouflageCoroutine = StartCoroutine(CamouflageTimer());
    }

    private void StopCamouflage()
    {
        isCamouflaged = false;
        playerObject.SetActive(true);
        camouflagedObject.SetActive(false);

        if (camouflageCoroutine != null)
        {
            StopCoroutine(camouflageCoroutine);
            camouflageCoroutine = null;
        }
    }

    private IEnumerator CamouflageTimer()
    {
        yield return new WaitForSeconds(camouflageDuration);
        StopCamouflage();
    }
    public void ThrowBomb(Vector2 force)
    {
        GameObject Bomb = Instantiate(bomb, SpawnPoint.position, Quaternion.identity);
        Bomb.GetComponent<Bomb>().sound = sfx;
        Bomb.GetComponent<Bomb>().player = this;
        Bomb.GetComponent<Bomb>().Push(force);
    }
    public void ApplyVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}
