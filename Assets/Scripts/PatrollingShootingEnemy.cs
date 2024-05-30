using UnityEngine;

public class PatrollingShootingEnemy : MonoBehaviour
{
    [Header("Variables comunes")]
    public float speed;
    public float cooldown;
    public float detectionRange;

    [Header("Variables específicas")]
    public GameObject balaPrefab;
    public float patrolDistance;
    public float summonBullet;
    public float bulletLifetime = 5f; // Tiempo de vida de la bala en segundos

    private float lastActionTime = 0f;
    private Transform player;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float distanceTraveled = 0f;
    private bool playerDetected = false;

    [Header("Parámetros de Gizmo")]
    public Color gizmoColor = Color.yellow;
    public bool drawGizmos = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerDetected)
        {
            FollowPlayer();
            ShootAtPlayer();
        }
        else
        {
            PatrolMove();
        }
    }

    private void PatrolMove()
    {
        Patrolling();
    }

    private void Patrolling()
    {
        Vector2 moveVelocity = facingRight ? Vector2.right : Vector2.left;
        moveVelocity *= speed;

        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);

        distanceTraveled += Mathf.Abs(rb.velocity.x * Time.deltaTime);

        if (distanceTraveled >= patrolDistance)
        {
            distanceTraveled = 0f;
            Flip();
        }
    }

    private void FollowPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 moveVelocity = directionToPlayer * speed;
        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);

        if ((moveVelocity.x > 0 && !facingRight) || (moveVelocity.x < 0 && facingRight))
        {
            Flip();
        }
    }

    private void ShootAtPlayer()
    {
        if (Time.time >= lastActionTime + cooldown)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            GameObject bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
            bala.GetComponent<Rigidbody2D>().velocity = directionToPlayer * summonBullet;

            // Destruye la bala después de 'bulletLifetime' segundos
            Destroy(bala, bulletLifetime);

            lastActionTime = Time.time;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        // Dibujar la distancia de patrulla
        Gizmos.color = Color.blue;
        Vector3 startPos = transform.position + Vector3.left * patrolDistance;
        Vector3 endPos = transform.position + Vector3.right * patrolDistance;
        Gizmos.DrawLine(startPos, endPos);

        // Dibujar el rango de detección
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
