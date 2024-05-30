using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public enum EnemyType
{
    FlyMove,
    SummonMove,
    PatrolMove,
    SniperMove,
    ChaserMove
}

public class EnemyController : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("Variables comunes")]
    public float speed;
    public float detectionRange;
    public float cooldown;

    [Header("Variables para FlyMove y SummonMove")]
    public float hoveringHeight;
    [Header("Variables para FlyMove y SniperMove")]
    public GameObject balaPrefab;
    [Header("Variables para FlyMove y SniperMove")]
    public float summonBullet;

    [Header("Variable para SniperMove")]
    public float escapeRange;

    [Header("Variables para PatrolMove y ChaserMove")]
    public float patrolDistance;

    private float lastActionTime = 0f;
    private Transform player;
    private Rigidbody2D rb;
    private SpawnEnemy spawner;
    private Animator anim;
    private bool facingRight = true;
    private float distanceTraveled = 0f;

    [Header("Parámetros de Gizmo")]
    public Color gizmoColor = Color.yellow;
    public bool drawGizmos = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyType == EnemyType.SummonMove)
        {
            spawner = GetComponent<SpawnEnemy>();
        }
        if (enemyType == EnemyType.SniperMove || enemyType == EnemyType.ChaserMove || enemyType == EnemyType.PatrolMove)
        {
            anim = GetComponentInChildren<Animator>();
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (enemyType)
        {
            case EnemyType.FlyMove:
                FlyMove();
                break;
            case EnemyType.SummonMove:
                SummonMove();
                break;
            case EnemyType.PatrolMove:
                PatrolMove();
                break;
            case EnemyType.SniperMove:
                SniperMove();
                break;
            case EnemyType.ChaserMove:
                ChaserMove();
                break;
        }
    }
   // private void DatosAnimator()
   // {
   //     float velocityX = Mathf.Abs(rb.velocity.x);
    //    anim.SetFloat("velocidadX", velocityX);
   // }

    private void FlyMove()
    {
        DetectPlayer();
        FlyTowardsPlayer();
    }
    private void SummonMove()
    {
        DetectPlayer();
        FlyTowardsPlayer();
    }
    private void PatrolMove()
    {
       // DatosAnimator();
        Patrolling();
    }
    private void SniperMove()
    {
       // DatosAnimator();
        DetectPlayer();
        EscapeFromPlayer();
    }
    private void ChaserMove()
    {
       // DatosAnimator();
        DetectPlayer();
        Patrolling();
    }
    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (Time.time >= lastActionTime + cooldown)
            {
                if (enemyType == EnemyType.FlyMove || enemyType == EnemyType.SniperMove)
                {
                    ShootAtPlayer();
                }
                else if (enemyType == EnemyType.SummonMove)
                {
                    spawner.SummonEnemies();
                }
                else if (enemyType == EnemyType.ChaserMove)
                {

                    Chase();
                }
                lastActionTime = Time.time;
            }
        }
    }
    void FlyTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        Vector2 flyingVelocity = direction * speed;

        rb.velocity = new Vector2(flyingVelocity.x, Mathf.Sin(Time.time) * hoveringHeight);

        if (flyingVelocity.x > 0 && facingRight)
        {
            Flip();
        }
        else if (flyingVelocity.x < 0 && !facingRight)
        {
            Flip();
        }
    }
    void Patrolling()
    {
        Vector2 moveVelocity = Vector2.zero;

        if (!facingRight)
        {
            moveVelocity = Vector2.right * speed;
        }
        else
        {
            moveVelocity = Vector2.left * speed;
        }

        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);


        distanceTraveled += Mathf.Abs(rb.velocity.x * Time.deltaTime);

        if (distanceTraveled >= patrolDistance)
        {
            distanceTraveled = 0f;
            Flip();
        }
    }
    void EscapeFromPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < escapeRange)
        {
            Vector3 escapeDirection = transform.position - player.position;

            escapeDirection.Normalize();

            rb.velocity = new Vector2(escapeDirection.x * speed, rb.velocity.y);
        }
        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }

    }
    void ShootAtPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bala = Instantiate(balaPrefab, transform.position - Vector3.down * summonBullet, rotation);
    }
    void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 moveVelocity = direction * speed;

        rb.velocity = new Vector2(moveVelocity.x, 0f);


        if (moveVelocity.x > 0 && facingRight)
        {
            Flip();
        }
        else if (moveVelocity.x < 0 && !facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        // Dibujar el rango de detección
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Dibujar el rango de escape
        if (enemyType == EnemyType.SniperMove)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, escapeRange);
        }

        // Dibujar la distancia de patrulla
        if (enemyType == EnemyType.PatrolMove || enemyType == EnemyType.ChaserMove)
        {
            Gizmos.color = Color.blue;
            Vector3 startPos = transform.position + Vector3.left * patrolDistance;
            Vector3 endPos = transform.position + Vector3.right * patrolDistance;
            Gizmos.DrawLine(startPos, endPos);
        }

        // Dibujar la altura de planeo
        if (enemyType == EnemyType.FlyMove || enemyType == EnemyType.SummonMove)
        {
            Gizmos.color = Color.green;
            Vector3 hoveringPos = transform.position + Vector3.up * hoveringHeight;
            Gizmos.DrawLine(transform.position, hoveringPos);
        }
    }
}
