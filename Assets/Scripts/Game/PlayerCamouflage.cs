using UnityEngine;
using UnityEngine.UI; // A�adir para usar UI
using System.Collections; // A�adido para IEnumerator

public class PlayerCamouflage : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    public GameObject playerObject;
    public GameObject camouflagedObject;

    [Header("Configuraci�n de Entrada")]
    public KeyCode camouflageKey = KeyCode.C;

    [Header("Duraci�n del Camuflaje")]
    public float camouflageDuration = 5.0f;

    private bool isCamouflaged = false;
    private Coroutine camouflageCoroutine;
    public bool canThrow;
    public Transform SpawnPoint;
    public float throwForce;
    public GameObject bomb;
    private Rigidbody2D rb;
    private Animation anim;
    [Header("Sonidos")]
    public AudioClip exitCamouflageSound; // Clip de sonido para salir del camuflaje
    private AudioSource sfx;

    private Vector2 currentVelocity;

    [Header("UI")]
    public Image camouflageCooldownImage; // Referencia a la imagen de UI

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        anim = playerObject.GetComponent<Animation>();
        camouflageCooldownImage.fillAmount = 0; // Inicialmente el valor es cero
    }

    void Update()
    {
        if (Input.GetKeyDown(camouflageKey))
        {
            ToggleCamouflage();
        }

        // Detectar la direcci�n del movimiento y voltear el sprite del jugador
        FlipPlayerObject();
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
        sfx.PlayOneShot(exitCamouflageSound);
        anim.Play("exitcamuflaje");
        camouflageCooldownImage.fillAmount = 0; // Restablecer el valor de la imagen
    }

    private IEnumerator CamouflageTimer()
    {
        float elapsedTime = 0;

        while (elapsedTime < camouflageDuration)
        {
            elapsedTime += Time.deltaTime;
            camouflageCooldownImage.fillAmount = 1 - (elapsedTime / camouflageDuration); // Invertir el valor de llenado de la imagen
            yield return null;
        }

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
        currentVelocity = velocity;
    }

    private void FlipPlayerObject()
    {
        float horizontalVelocity = currentVelocity.x;

        if (horizontalVelocity != 0)
        {
            Vector3 scale = playerObject.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (horizontalVelocity > 0 ? 1 : -1);
            playerObject.transform.localScale = scale;

            Vector3 scale1 = camouflagedObject.transform.localScale;
            scale1.x = Mathf.Abs(scale1.x) * (horizontalVelocity > 0 ? 1 : -1);
            camouflagedObject.transform.localScale = scale1;
        }
    }
}
